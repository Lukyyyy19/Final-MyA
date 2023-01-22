using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventManager : MonoBehaviour {
    public static EventManager instance;

    public Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();

    private void Awake() {
        Debug.Log("instancia event manager creada");
        instance = this;
    }

    public void AddAction(string actionName, Action action) {
        if (eventDictionary.ContainsKey(actionName)) {
            eventDictionary[actionName] += action;
            Debug.Log($"añadiendo metodo a la key {actionName}");
        } else {
            eventDictionary.Add(actionName, action);
            print($"Creando key {actionName} y añadiendo");
        }
    }


    public void RemoveAction(string actionName, Action action) {
        eventDictionary[actionName] -= action;
    }


    public void TriggerEvent(string actionName) {
        if (eventDictionary.ContainsKey(actionName))
            eventDictionary[actionName]?.Invoke();
        else
            Debug.Log($"No contiene la key {actionName}");
    }

}
