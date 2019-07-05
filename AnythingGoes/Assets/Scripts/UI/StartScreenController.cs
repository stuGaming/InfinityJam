using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    [SerializeField]
    Button LoadGameBuuton;
    public void ClickNewGame()
    {

    }
    public void ClickLoadGame()
    {

    }
    public void ClickGameCredits()
    {

    }
    public void ClickExitGame()
    {
        Application.Quit();
    }
    private void Start()
    {
        if (GameResources.Instance.CurrentGameLevel>0)
        {
            LoadGameBuuton.interactable = true;
        }
        else
        {
            LoadGameBuuton.interactable = false;
        }
    }

}
