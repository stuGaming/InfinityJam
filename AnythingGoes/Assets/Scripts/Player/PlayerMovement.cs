using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D thisRigid;
    [SerializeField]
    private float maxForce;
    // Use this for initialization
    void Start()
    {
        thisRigid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void ApplyForce(float v, Vector2 targetDirection)
    {
        thisRigid.AddForce((-1*targetDirection )* (maxForce * v),ForceMode2D.Impulse);
        // apply force
    }
}
