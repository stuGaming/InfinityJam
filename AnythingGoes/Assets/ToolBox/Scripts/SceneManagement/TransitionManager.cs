using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;
    public TransitionAnimationController transition;
    void Awake()
    {
        DontDestroyOnLoad(this);
        
        if (Instance != null)
        {
            Debug.Log("TransitionManger already exists, destroying this");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
    }
    TransitionEndPoint currentEndPoint;

    Message currentMessageToSend;
    string nextSceneName;
    public  void LoadScene(string sceneName)
    {
        nextSceneName =sceneName;
        transition.BeginTransition(()=> { TransitionMidPoint(); });
        

    }

    public void TransitionCompeleted()
    {
        transition.FinishTransition();
    }

    private void TransitionMidPoint()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void AddToMessage(params object[] args)
    {
        if (currentMessageToSend == null) {
            currentMessageToSend = new Message();
        }

        currentMessageToSend.AddToPayload(args);
    }

    internal void SetCurrentScene(TransitionEndPoint transitionEndPoint)
    {
        transitionEndPoint.TransitionComplete(currentMessageToSend);
    }
}
