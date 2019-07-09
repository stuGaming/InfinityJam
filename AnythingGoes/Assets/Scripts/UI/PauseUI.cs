using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PauseUI : BaseUI
{
    public List<GameObject> instructionObjects = new List<GameObject>();
    public List<GameObject> pauseObjects = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        Mediator.RegisterHandler(GameUIEvents.ShowPausePanel, this, ShowPause);
        Mediator.RegisterHandler(GameUIEvents.HidePausePanel, this, HidePause);
        Hide();
    }

    private void HidePause(Message message)
    {
        Debug.Log("Hiding pause");
        Hide(); 
    }

    private void ShowPause(Message message)
    {
        Show();
        Debug.Log("Showing pause");
        ReturnToPause();
    }
    

    public void UnpauseClick()
    {
        LevelController.Instance.UnpauseAll();
        Hide();
    }
    public void ExitLevel()
    {
        TransitionManager.Instance.LoadScene("StartScreen");
    }
    public void ShowInstructions()
    {
        foreach(GameObject g in instructionObjects)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }
    public void ReturnToPause()
    {
        foreach (GameObject g in instructionObjects)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
