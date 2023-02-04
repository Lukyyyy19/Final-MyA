using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventManager : MonoBehaviour {
    public static EventManager instance;

    public Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();

    private void Awake() {
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
        // eventDictionary[actionName] -= action;
        if (eventDictionary.ContainsKey(actionName)) {
            eventDictionary[actionName] -= action;
            Debug.Log($"Removiendo metodo a la key {actionName}");
        } else {
            //eventDictionary.Add(actionName, action);
            print($"No existe la key {actionName} para remover");
        }
    }


    public void TriggerEvent(string actionName) {
        if (eventDictionary.ContainsKey(actionName)) {
            eventDictionary[actionName]?.Invoke();
            Debug.Log($"LLamando al evento {actionName}");
        } else
            Debug.Log($"No contiene la key {actionName}");
    }

}
