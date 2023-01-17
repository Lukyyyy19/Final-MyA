using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    private bool isPaused = true;
    private void Awake()
    {
        instance = this;
    }

    HashSet<IPausable> pausables = new HashSet<IPausable>();

    public void AddPausable(IPausable pausable) => pausables.Add(pausable);
    public void RemovePausable(IPausable pausable) => pausables.Remove(pausable);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            isPaused = !isPaused;
        switch (isPaused)
        {
            case false:
                Pause();
                break;
            default:
                Resume();
                break;
        }
    }

    public void Pause()
    {
        foreach (var item in pausables)
        {
            item.Pause();
        }
    }

    public void Resume()
    {
        foreach (var item in pausables)
        {
            item.Resume();
        }
    }
}
