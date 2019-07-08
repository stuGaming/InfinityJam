using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    internal float currentCharge;
    
    internal float modifiedCharge;
    [SerializeField]
    float maxHealth;
    [SerializeField]
    internal float maxWeaponCharge;
    private float _currentHealth;
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            
            _currentHealth = value;
            if (_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
            }
            if (_currentHealth<=0)
            { 
                Mediator.SendMessage(GameEvents.ResetPlayer);
                CurrentHealth = maxHealth;
            }
            else
            {
                Mediator.SendMessage(GameUIEvents.HealthUpdate,GameUIEventProperties.Health,CurrentHealth/maxHealth);
            }
        }
    }
    public void Damage(float damage)
    {
        CurrentHealth -= damage;
    }
    public void ResetCharge()
    {
        float charge = currentCharge - modifiedCharge;
        currentCharge = maxWeaponCharge;
        modifiedCharge = currentCharge - charge;
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
        GameResources.Instance.RegisterPlayer(this);
    }

    
}
