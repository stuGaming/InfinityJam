using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionEndPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    protected void Setup()
    {
        Debug.Log("Transition end point loaded");
        if (TransitionManager.Instance != null)
        {
            TransitionManager.Instance.SetCurrentScene(this);
        }
    }
    public void TransitionComplete(Message message)
    {
        //Handle message here
        TransitionManager.Instance.TransitionCompeleted();
    }


   
}
