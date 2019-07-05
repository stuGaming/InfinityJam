using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    PlayerMovement movement;
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    float maxWeaponStrength;
    [SerializeField]
    float minWeaponStrength;
    [SerializeField]
    float maxWeaponCharge;

    [SerializeField]
    GameObject hand;
    float charge;


    bool charging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 targetDirection = (playerInput.InputPosition-(Vector2)this.transform.position).normalized;
        bool firing = playerInput.Firing;
        if (!charging && firing)
        {
            charging = true;
            charge = Time.deltaTime;
            // Begin charging 

        }else if (charging && !firing)
        {
            Debug.Log("Firing Weapon");
            FireWeapon(charge, targetDirection);
            charging = false;
            // Fire weapon
        }else if (charging && firing)
        {
            charge += Time.deltaTime;
            if (charge > maxWeaponCharge)
            {
                charge = maxWeaponCharge;
            }
            // Continue to charge weapon
        }
        LookAt(targetDirection);
    }

    private void LookAt(Vector2 targetDirection)
    {
        //targetDirection.z = hand.transform.position.z;
       
        hand.transform.right = targetDirection;
        // TODO point weapon in direction
    }

    private void FireWeapon(float charge, Vector2 targetDirection)
    {
        movement.ApplyForce(charge / maxWeaponCharge, targetDirection);

        // Attack damage
    }
}
