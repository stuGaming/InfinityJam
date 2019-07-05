using UnityEngine;
using System.Collections;
using System;

public class CheckPointController : MonoBehaviour
{
    [SerializeField]
    Transform lastCheckPoint;
    [SerializeField]
    PlayerController player;
    // Use this for initialization
    void Start()
    {
        Mediator.RegisterHandler(GameEvents.CheckPointReached, this , checkPointReached);
        Mediator.RegisterHandler(GameEvents.ResetPlayer, this, ResetPlayer);
    }

    private void ResetPlayer(Message message)
    {
        player.transform.position = lastCheckPoint.position;
        player.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void checkPointReached(Message message)
    {
        lastCheckPoint = message.Properties[GameEventProperties.CheckPoint] as Transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
