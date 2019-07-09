using UnityEngine;
using System.Collections;
using System;

public class LevelCompleteUI : BaseUI
{

    // Use this for initialization
    void Start()
    {
        Mediator.RegisterHandler(GameUIEvents.ShowLevelComplete,this,showPanel);
        Hide();
    }

    public void ContinueClick()
    {
        if (GameResources.Instance.CurrentGameLevel >= GameResources.Instance.MaxLevels)
        {
            TransitionManager.Instance.LoadScene("StartScreen");
        }
        else
        {
            GameResources.Instance.CurrentGameLevel++;
            TransitionManager.Instance.LoadScene("Level"+(GameResources.Instance.CurrentGameLevel));
        }
    }

    private void showPanel(Message message)
    {
        Show();
    }

}
