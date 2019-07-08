using UnityEngine;
using System.Collections;

public class BasePickup : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected virtual void PickupWorker(PlayerStats stats)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats stats = collision.transform.GetComponent<PlayerStats>();
        if (stats != null)
        {
            PickupWorker(stats);
            Destroy(this.gameObject);
        }

    }
}
