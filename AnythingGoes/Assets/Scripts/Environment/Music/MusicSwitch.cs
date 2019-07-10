using UnityEngine;
using System.Collections;

public class MusicSwitch : MonoBehaviour
{
    public int switchIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Mediator.SendMessage(GameEvents.AudioSwitch,GameEventProperties.Audio, switchIndex);

    }
    
}
