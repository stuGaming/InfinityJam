using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    public PlayerStats player;
    public bool GameIsPlaying
    {
        get
        {
            return GameIsStarted && !GameIsPaused;
        }

    }
    public bool GameIsStarted = false;
    public bool GameIsPaused {
        get
        {
            return pauseObjects.Count > 0;
        }
    }
    public List<string> pauseObjects = new List<string>();

    // Use this for initialization
    void Start()
    {
        Instance = this;
        Mediator.RegisterHandler(GameEvents.PauseGame,this,PauseGame);
        Mediator.RegisterHandler(GameEvents.UnPauseGame, this, UnPauseGame);
    }

    private void UnPauseGame(Message message)
    {
        string name = (string)message.Properties[GameEventProperties.Name];
        if (pauseObjects.Contains(name))
        {
            pauseObjects.Remove(name);
            Mediator.SendMessage(GameEvents.PauseStateChanged);
        }
    }

    private void PauseGame(Message message)
    {
        string name = (string)message.Properties[GameEventProperties.Name];
        if (!pauseObjects.Contains(name))
        {
            pauseObjects.Add(name);
            Mediator.SendMessage(GameEvents.PauseStateChanged);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        Instance = null;
    }
    public void RegisterPlayer(PlayerStats stats)
    {
        player = stats;
    }
}
