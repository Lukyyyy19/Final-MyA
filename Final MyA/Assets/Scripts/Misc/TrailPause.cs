using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPause : MonoBehaviour, IPausable {
    TrailRenderer tr;
    float time = .1f;
    public void Pause() {
        tr.time = 0;
    }

    public void Resume() {
        tr.time = time;
    }

    private void Awake() {
        tr = GetComponent<TrailRenderer>();
    }
    void Start() {
        ScreenManager.instance.AddPausable(this);
    }
    private void OnDisable() {
        ScreenManager.instance.RemovePausable(this);
    }
}
