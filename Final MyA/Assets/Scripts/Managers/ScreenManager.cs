using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {
    public static ScreenManager instance;
    public bool isPaused;
    private void Awake() {
        instance = this;
    }

    HashSet<IPausable> pausables = new HashSet<IPausable>();

    public void AddPausable(IPausable pausable) => pausables.Add(pausable);
    public void RemovePausable(IPausable pausable) => pausables.Remove(pausable);

    public void Pause() {
        isPaused = true;
        foreach (var item in pausables) {
            item.Pause();
        }
    }

    public void Resume() {
        isPaused = false;
        foreach (var item in pausables) {
            item.Resume();
        }
    }

    public void ChangeScene(string SceneName) {
        SceneManager.LoadScene(SceneName);
    }
}
