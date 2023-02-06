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

    [SerializeField]
    Button _btnAbility1;
    [SerializeField]
    Button _btnAbility2;
    [SerializeField]
    Button _btnAbility3;




    [SerializeField]
    TextMeshProUGUI _textAbility1;
    [SerializeField]
    TextMeshProUGUI _textAbility2;
    [SerializeField]
    TextMeshProUGUI _textAbility3;


    private void Start() {
        _treeSkills = PlayerManager.instance.TreeSkills;
    }
    private void Update() {
        _abilityPointsTxt.text = _treeSkills.abilityPoints.ToString();
    }
    public void UnlockSkill(int skills) {
        _treeSkills.TryUnlockSkill((PlayerSkills)skills);
    }

    public void UpdateAbilitiesText() {

        var ability1 = _treeSkills.GetRandomAbility();
        var ability2 = _treeSkills.GetRandomAbility();
        var ability3 = _treeSkills.GetRandomAbility();

        _btnAbility1.onClick.AddListener(() => UnlockSkill((int)ability1));
        _btnAbility2.onClick.AddListener(() => UnlockSkill((int)ability2));
        _btnAbility3.onClick.AddListener(() => UnlockSkill((int)ability3));

        _textAbility1.text = ability1.ToString();
        _textAbility2.text = ability2.ToString();
        _textAbility3.text = ability3.ToString();
    }
}
