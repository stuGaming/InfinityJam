﻿using UnityEngine;
using System.Collections;
using System;

public class GrapplingGun : MonoBehaviour
{
    [SerializeField]
    LineRenderer Chain;
    [SerializeField]
    GameObject hook;
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    Transform muzzle;
    [SerializeField]
    float pullForce;
    [SerializeField]
    float breakDistance;
    private Collider2D latchedObject;
    private bool _chainExtended = false;
    private Rigidbody2D thisRigid;
    private bool ChainExtended
    {
        get
        {
            return _chainExtended;
        }
        set
        {
            _chainExtended = value;
            Chain.gameObject.SetActive(_chainExtended);
           
           
            // hook.SetActive(_chainExtended);
        }
    }
    // Use this for initialization
    void Start()
    {
        thisRigid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        bool fireChain =playerInput.SecondaryFire;
        bool releaseChain = playerInput.SecondaryReleased;
        if (fireChain)
        {
            SetupChain();
        }
        if (ChainExtended)
        {

            CheckBreak();
            if (ChainExtended)
            {
                

                UpdateChainPositions();
            }
               
            
        }
        if (releaseChain)
        {
            ChainExtended = false;
        }
    }

    private void PullTowards()
    {
        Debug.Log("Pulling");
        thisRigid.AddForce((pullForce * Time.deltaTime * (hook.transform.position - this.transform.position).normalized));
        //thisRigid.velocity = Vector3.zero;
       // thisRigid.MovePosition(this.transform.position + (pullForce * Time.deltaTime * (hook.transform.position - this.transform.position)));
    }
    private void FixedUpdate()
    {
        if (ChainExtended)
        {
            PullTowards();
        }
    }
    private void CheckBreak()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position,    hook.transform.position - this.transform.position, 9);
        Debug.DrawLine(this.transform.position, hook.transform.position, Color.red);
        if (hit.collider != latchedObject)
        {
            
            ChainExtended = false;
            Debug.Log("Released1");
        }
        else if (Mathf.Abs((hook.transform.position - this.transform.position).magnitude) < breakDistance)
        {
            ChainExtended = false;
        }
        if (hit.collider == null)
        {
            Debug.Log("Missed");
        }
    }

    private void UpdateChainPositions()
    {
        Chain.SetPosition(1, this.transform.position);
        Chain.SetPosition(0, hook.transform.position);
        hook.transform.right = hook.transform.position - this.transform.position;
    }

    private void SetupChain()
    {
        RaycastHit2D hit = Physics2D.Raycast(muzzle.position, muzzle.right);
        if (hit.collider!=null)
        {
            latchedObject = hit.collider;
            ChainExtended = true;
            Chain.positionCount = 2;
            hook.transform.position = hit.point;
            Chain.SetPosition(1, this.transform.position);
            Chain.SetPosition(0, hook.transform.position);
            Debug.Log("Latched");
            //thisRigid.velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("Missed all");
            ChainExtended = false;
        }
       
    }
}