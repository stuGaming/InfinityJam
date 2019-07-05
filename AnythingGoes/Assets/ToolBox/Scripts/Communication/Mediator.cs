using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Stuart McRoberts
//Messaging system to communicate between different projects
public class Mediator : MonoBehaviour
{
    public static Mediator Instance = null;


    public static Dictionary<KeyValuePair<object,object>, List<CommunicationHandler>> RegisteredHandlers = new Dictionary<KeyValuePair<object, object>, List<CommunicationHandler>>();



    public static Dictionary<object, List<object>> RegisteredObjects = new Dictionary<object, List<object>>();

    public delegate void CommunicationHandler(Message message);
    private void Awake()
    {
        Debug.Log(this.transform.name);
        if (Mediator.Instance != null)
        {
            Destroy(this);

        }
        else
        {
            DontDestroyOnLoad(this);
            Mediator.Instance = this;
        }
    }

    public static void SendMessage(object Identification, params object[] args)
    {
        Message msg = new Message();
        if (args.Length > 1)
        {

            msg.CreatePayload(args);

        } 
        foreach(KeyValuePair<object,object> key in RegisteredHandlers.Keys)
        {
            
            if ((key.Key.GetType()==Identification.GetType())&&(int)key.Key == (int)Identification)
            {
                foreach (CommunicationHandler handler in RegisteredHandlers[key])
                {
                    handler(msg);
                }
            }
        }
        
    }

    public static void RegisterHandler(object Identification, object baseObject, CommunicationHandler funct)
    {
        KeyValuePair<object,object> key = new KeyValuePair<object, object>(Identification,baseObject);
        if (RegisteredHandlers.ContainsKey(key))
        {
            RegisteredHandlers[key].Add(funct);
        }
        else
        {
            RegisteredHandlers.Add(key, new List<CommunicationHandler>());
            RegisteredHandlers[key].Add(funct);
        }
       

    }
    /// <summary>
    /// Unregisters all communication handlers that are associated with a specific object
    /// </summary>
    /// <param name="baseObject"></param>
    public static void UnRegisterAllHandlers(object baseObject)
    {
        List<KeyValuePair<object, object>> keys = new List<KeyValuePair<object, object>>();
        foreach(KeyValuePair<object,object> key in RegisteredHandlers.Keys)
        {
            if (key.Value == baseObject)
            {
                keys.Add(key);
            }
        }
        foreach(KeyValuePair<object,object> key in keys)
        {
            RegisteredHandlers.Remove(key);
        }
    }
    /// <summary>
    /// Removes a specific handler associated to specific listener
    /// </summary>
    /// <param name="baseObject"></param>
    /// <param name="identification"></param>
    public static void UnRegisterHandler(object identification,object baseObject)
    {
        KeyValuePair<object, object> key = new KeyValuePair<object, object>(identification, baseObject);
        if (RegisteredHandlers.ContainsKey(key))
        {
            RegisteredHandlers.Remove(key);
        }
    }

   
}
