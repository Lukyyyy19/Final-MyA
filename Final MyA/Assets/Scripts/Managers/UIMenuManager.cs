using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMenuManager : MonoBehaviour {
    [SerializeField]
    GameObject _pauseMenu;

    public void ShowPauseMenu() {
        _pauseMenu.SetActive(true);
    }
    public void DeactivatePauseMenu() {
        _pauseMenu.SetActive(false);
    }

}
