using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UpgradesEnum;
public class newUITreeSkill : MonoBehaviour {
    [SerializeField]
    private TreeSkills _treeSkills;

    [SerializeField]
    Button _btnAbility1;
    [SerializeField]
    Button _btnAbility2;
    [SerializeField]
    Button _btnAbility3;

    [SerializeField]
    TextMeshProUGUI _txtAbility1;
    [SerializeField]
    TextMeshProUGUI _txtAbility2;
    [SerializeField]
    TextMeshProUGUI _txtAbility3;

    private PlayerSkills _ability1;
    private PlayerSkills _ability2;
    private PlayerSkills _ability3;

    void Start() {
        _treeSkills = PlayerManager.instance.TreeSkills;
    }

    public void UnlockSkill(PlayerSkills skills) {
        _treeSkills.TryUnlockSkill(skills);
    }

    public void UpdateAbilitiesText() {
        if (_treeSkills.PlayerSkillsCount() < _treeSkills.GetMaxLevel()) {
            var randomAbilityList = _treeSkills.GetRandomAbility();
            _btnAbility1.gameObject.SetActive(true);
            _ability1 = randomAbilityList[0];
            var ability1Text = _ability1.ToString();
            _txtAbility1.text = ability1Text.Replace('_', ' ');
            if (randomAbilityList.Count > 1) {
                _btnAbility2.gameObject.SetActive(true);
                _ability2 = randomAbilityList[1];
                var ability2Text= _ability2.ToString();
                _txtAbility2.text = ability2Text.Replace('_', ' ');
            }

            if (randomAbilityList.Count > 2) {
                _btnAbility3.gameObject.SetActive(true);
                _ability3 = randomAbilityList[2];
                var ability3Text = _ability3.ToString();
                _txtAbility3.text = ability3Text.Replace('_', ' ');
            }

            _btnAbility1.onClick.AddListener(() => UnlockSkill(_ability1));
            _btnAbility2.onClick.AddListener(() => UnlockSkill(_ability2));
            _btnAbility3.onClick.AddListener(() => UnlockSkill(_ability3));



        }
    }


}
