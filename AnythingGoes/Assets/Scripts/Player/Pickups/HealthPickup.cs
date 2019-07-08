using UnityEngine;
using System.Collections;

public class HealthPickup : BasePickup
{
    protected override void PickupWorker(PlayerStats stats)
    {
        stats.CurrentHealth += 30;
        base.PickupWorker(stats);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
