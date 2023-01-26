using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesPause : MonoBehaviour, IPausable {
    ParticleSystem ps;
    public void Pause() {
        ps.Pause();
    }

    public void Resume() {
        ps.Play();
    }

    private void OnDestroy() {
        ScreenManager.instance.RemovePausable(this);
    }

    void Awake() {
        ps = GetComponent<ParticleSystem>();
    }
    private void Start() {
        ScreenManager.instance.AddPausable(this);
    }
}
