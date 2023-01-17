using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public Dictionary<string, Action> eventDictionary;
    public Dictionary<string, Action<EventArgs>> eventArgsDictionary;
    private void OnEnable()
    {
        instance = this;
        eventDictionary = new Dictionary<string, Action>();
        eventArgsDictionary = new Dictionary<string, Action<EventArgs>>();
    }

    public void AddAction(string actionName, Action action)
    {
        if (eventDictionary.ContainsKey(actionName))
        {
            eventDictionary[actionName] += action;
            Debug.Log("añadiendo");
        }
        else
        {
            eventDictionary.Add(actionName, action);
            print($"Creando key{actionName} y añadiendo");
        }
    }
    public void AddAction(string actionName, Action<EventArgs> action)
    {
        if (eventDictionary.ContainsKey(actionName))
        {
            eventArgsDictionary[actionName] += action;
            Debug.Log("añadiendo");
        }
        else
        {
            eventArgsDictionary.Add(actionName, action);
            print($"Creando key{actionName} y añadiendo");
        }
    }

    public void RemoveAction(string actionName, Action action)
    {
        eventDictionary[actionName] -= action;
    }
    public void RemoveAction(string actionName, Action<EventArgs> action)
    {
        eventArgsDictionary[actionName] -= action;
    }

    public void TriggerEvent(string actionName)
    {
        eventDictionary[actionName]?.Invoke();
    }
    public void TriggerEvent(string actionName, EventArgs eventArgs)
    {
        eventArgsDictionary[actionName]?.Invoke(eventArgs);
    }
}
