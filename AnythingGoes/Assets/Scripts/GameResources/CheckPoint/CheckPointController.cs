using UnityEngine;
using System.Collections;
using System;

public class CheckPointController : MonoBehaviour
{
    [SerializeField]
    Transform lastCheckPoint;
    Vector3 respawnPosition;
    [SerializeField]
    PlayerController player;
    // Use this for initialization
    void Start()
    {
        Mediator.RegisterHandler(GameEvents.CheckPointReached, this , checkPointReached);
        Mediator.RegisterHandler(GameEvents.ResetPlayer, this, ResetPlayer);
        respawnPosition = lastCheckPoint.position;
    }

    private void ResetPlayer(Message message)
    {
        player.transform.position = respawnPosition;
        player.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void checkPointReached(Message message)
    {
        lastCheckPoint = (Transform)message.Properties[GameEventProperties.CheckPoint];
        respawnPosition = lastCheckPoint.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
