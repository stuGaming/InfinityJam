using UnityEngine;
using System.Collections;
using System;

public class PausableRigidbody : MonoBehaviour
{

     Rigidbody2D thisRigid;
    Vector2 velocity;
    float angularVelocity;
    private void Start()
    {
        Mediator.RegisterHandler(GameEvents.PauseStateChanged, this, PauseChanged);

    }

    private void PauseChanged(Message message)
    {
        if (LevelController.Instance.GameIsPlaying)
        {
            velocity = thisRigid.velocity;
            angularVelocity = thisRigid.angularVelocity;
            thisRigid.simulated = false;
        }
        else
        {
            thisRigid.simulated = true;
            thisRigid.velocity = velocity;
            thisRigid.angularVelocity = angularVelocity;
        }
    }

    private void OnDestroy()
    {
        Mediator.UnRegisterAllHandlers(this);
    }
}
