using UnityEngine;
using System.Collections;

public class InstantKillTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats stats = collision.transform.GetComponent<PlayerStats>();
        if(stats!=null)
            stats.CurrentHealth = 0;
    }
    
}
