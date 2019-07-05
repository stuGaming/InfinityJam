using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    GrapplingGun grapplingGun;
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

    float currentCharge;
    float modifiedCharge;

    bool charging = false;
    // Start is called before the first frame update
    void Start()
    {
        currentCharge = maxWeaponCharge;
        modifiedCharge = maxWeaponCharge;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 targetDirection = (playerInput.InputPosition-(Vector2)this.transform.position).normalized;
        bool firing = playerInput.Firing;
        if(!grapplingGun.grappling||charging)
            currentCharge += Time.deltaTime *Mathf.Clamp(currentCharge/ maxWeaponCharge, 0.1f,0.6f);
        if (currentCharge > maxWeaponCharge)
            currentCharge = maxWeaponCharge;
        if (!charging && firing)
        {
            charging = true;
            charge = Time.deltaTime;
            // Begin charging 
           
        }
        else if (charging && !firing)
        {
            Debug.Log("Firing Weapon");
            currentCharge -= charge;
            modifiedCharge = currentCharge;
            FireWeapon(charge, targetDirection);
            charging = false;
            // Fire weapon
        }else if (charging && firing)
        {
            charge += Time.deltaTime;
            if (charge > currentCharge)
            {
                charge = currentCharge;
            }
            modifiedCharge = currentCharge - charge;
            Debug.Log(modifiedCharge + "");
            // Continue to charge weapon
        }
        else
        {
            modifiedCharge = currentCharge;
        }
        LookAt(targetDirection);
        Mediator.SendMessage(GameUIEvents.UpdateWeaponCharge,GameUIEventProperties.AvailableCharge, modifiedCharge/maxWeaponCharge,GameUIEventProperties.PreviousCharge,currentCharge / maxWeaponCharge);
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
