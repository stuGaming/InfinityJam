using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats stats = collision.transform.GetComponent<PlayerStats>();
        if (stats != null)
        {
            Mediator.SendMessage(GameEvents.CheckPointReached, GameEventProperties.CheckPoint, this.transform);
            Destroy(this.gameObject);
        }
        
    }
}
