using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMenuManager : MonoBehaviour {
    [SerializeField]
    GameObject _pauseMenu;
    [SerializeField]
    GameObject _treeMenu;
    [SerializeField]
    GameObject panel;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    public bool choosingAbility;


    public string TimeText { set => _timeText.text = value; }

    public void DeactivatePauseMenu() {
        panel.SetActive(false);
        _pauseMenu.SetActive(false);
    }
    public void ShowPauseMenu() {
        panel.SetActive(true);
        _pauseMenu.SetActive(true);
    }
    public void ShowTreeMenu() {
        choosingAbility = true;
        panel.SetActive(true);
        _treeMenu.SetActive(true);
    }
    public void DeactivateTreeMenu() {
        choosingAbility = false;
        // panel.SetActive(false);
        // _treeMenu.SetActive(false);
    }

}
