using UnityEngine;
using System.Collections;

public class LevelCompleteTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats stats = collision.transform.GetComponent<PlayerStats>();
        if (stats != null)
        {
            LevelController.Instance.EndGame();
            Mediator.SendMessage(GameUIEvents.ShowLevelComplete);
        }
    }
}
