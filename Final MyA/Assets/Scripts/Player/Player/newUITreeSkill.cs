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
        if (_treeSkills.SkillsUnlockedCount() < 7) {
            var randomAbilityList = _treeSkills.GetRandomAbility();
            _btnAbility1.gameObject.SetActive(true);
            _ability1 = randomAbilityList[0];
            _txtAbility1.text = _ability1.ToString();
            if (randomAbilityList.Count > 1) {
                _btnAbility2.gameObject.SetActive(true);
                _ability2 = randomAbilityList[1];
                _txtAbility2.text = _ability2.ToString();
            }

            if (randomAbilityList.Count > 2) {
                _btnAbility3.gameObject.SetActive(true);
                _ability3 = randomAbilityList[2];
                _txtAbility3.text = _ability3.ToString();
            }

            _btnAbility1.onClick.AddListener(() => UnlockSkill(_ability1));
            _btnAbility2.onClick.AddListener(() => UnlockSkill(_ability2));
            _btnAbility3.onClick.AddListener(() => UnlockSkill(_ability3));



        }
    }


}
