using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
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
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
    }

    
}
