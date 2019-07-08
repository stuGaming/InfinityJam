using UnityEngine;
using System.Collections;

public class ChargeRefresh : BasePickup
{
    protected override void PickupWorker(PlayerStats stats)
    {
        stats.ResetCharge();

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
