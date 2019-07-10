using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    public float Health;
    public float Damage;
    public void TakeDamage(float x)
    {
        Health -= x;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
}
