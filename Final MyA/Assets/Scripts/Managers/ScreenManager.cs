using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {
    public static ScreenManager instance;

    private void Awake() {
        instance = this;
    }

    HashSet<IPausable> pausables = new HashSet<IPausable>();

    public void AddPausable(IPausable pausable) => pausables.Add(pausable);
    public void RemovePausable(IPausable pausable) => pausables.Remove(pausable);

    public void Pause() {
        Debug.Log("Pause");
        foreach (var item in pausables) {
            item.Pause();
        }
    }

    public void Resume() {
        foreach (var item in pausables) {
            item.Resume();
        }
    }
}
