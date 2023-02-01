using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UpgradesEnum;

public class UiTreeSkill : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _abilityPointsTxt;
    private TreeSkills _treeSkills;
    private void Start() {
        _treeSkills = PlayerManager.instance.TreeSkills;
    }
    private void Update() {
        _abilityPointsTxt.text = _treeSkills.abilityPoints.ToString();
    }
    public void UnlockSkill(int skills) {
        _treeSkills.TryUnlockSkill((PlayerSkills)skills);
    }
    public void UnlockGunUprgrades(int upgrades) {

    }
}
