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

    internal void UnpauseAll()
    {
        pauseObjects = new List<string>();
        Mediator.SendMessage(GameEvents.PauseStateChanged);
    }
    public void StartGame()
    {
        GameIsStarted = true;
        pauseObjects = new List<string>();
        Mediator.SendMessage(GameEvents.PauseStateChanged);

    }
    public void EndGame()
    {
        GameIsStarted = false;
        pauseObjects = new List<string>();
        Mediator.SendMessage(GameEvents.PauseStateChanged);

    }
    void Awake()
    {
        Instance = this;
    }
        // Use this for initialization
    void Start()
    {
       
        Mediator.RegisterHandler(GameEvents.PauseGame,this,PauseGame);
        Mediator.RegisterHandler(GameEvents.UnPauseGame, this, UnPauseGame);
        Invoke("StartGame", 3);
    }

    private void UnPauseGame(Message message)
    {
        string name = (string)message.Properties[GameEventProperties.Name];
        if (pauseObjects.Contains(name))
        {
            pauseObjects.Remove(name);
            Mediator.SendMessage(GameEvents.PauseStateChanged);
        }
        if (GameIsStarted && GameIsPaused)
        {
            Mediator.SendMessage(GameUIEvents.ShowPausePanel);
        }
        else if(GameIsStarted&&!GameIsPaused)
        {
            Mediator.SendMessage(GameUIEvents.HidePausePanel);
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
        if (GameIsStarted && GameIsPaused)
        {
            Mediator.SendMessage(GameUIEvents.ShowPausePanel);
        }
        else if (GameIsStarted && !GameIsPaused)
        {
            Mediator.SendMessage(GameUIEvents.HidePausePanel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)&&GameIsStarted&&!GameIsPaused)
        {
            Mediator.SendMessage(GameEvents.PauseGame,GameEventProperties.Name,"LevelController");
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && GameIsStarted && GameIsPaused)
        {
            Mediator.SendMessage(GameEvents.UnPauseGame, GameEventProperties.Name, "LevelController");
        }
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
