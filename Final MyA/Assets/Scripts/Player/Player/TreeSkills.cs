using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesEnum;
using System;
using System.Linq;
using UnityEngine.Serialization;

[Serializable]


public class TreeSkills {
    [SerializeField]
    List<PlayerSkills> _allSkills = new List<PlayerSkills>();
     [SerializeField]
    List<PlayerSkills> _playerSkills = new List<PlayerSkills>();
    [SerializeField]
    List<PlayerSkills> _allowedSkills = new List<PlayerSkills>();
    [SerializeField]
    List<PlayerSkills> _randomSkills = new List<PlayerSkills>();
    [SerializeField]
    
    public Action<PlayerSkills> OnSkillUnlocked;
    public void Init() {
        _allSkills.Add(PlayerSkills.Dash);
        _allSkills.Add(PlayerSkills.Health_1);
        _allSkills.Add(PlayerSkills.Health_2);
        _allSkills.Add(PlayerSkills.Two_Shots);
        _allSkills.Add(PlayerSkills.FireRate);
        _allSkills.Add(PlayerSkills.Damage);
    }

    public List<PlayerSkills> GetRandomAbility() {
        _randomSkills.Clear();
        _allowedSkills = _allSkills.Where(skill => !PlayerHasSkill(skill) && IsUnlockedForPlayer(skill)).ToList();

        var numberOfRepetitions = _allowedSkills.Count > 3 ? 3 : _allowedSkills.Count;
        while (_randomSkills.Count < numberOfRepetitions) {
            int ab = UnityEngine.Random.Range(0, _allowedSkills.Count);
            var ps = _allowedSkills[ab];
            _allowedSkills.Remove(ps);
            _randomSkills.Add(ps);
        }
        return _randomSkills;
    }


    public bool TryUnlockSkill(PlayerSkills skill) {
        var skillType = GetDependentSkill(skill);
        if (skillType != PlayerSkills.None) {
            if (PlayerHasSkill(skillType)) {
                AddToPlayerSkills(skill);
                return true;
            }
            else {
                return false;
            }
        }
        else {
            AddToPlayerSkills(skill);
            return true;
        }
    }
    private void AddToPlayerSkills(PlayerSkills skill)
    {
        if (PlayerHasSkill(skill)) return;
        _playerSkills.Add(skill);
        OnSkillUnlocked?.Invoke(skill);
    }

    private PlayerSkills GetDependentSkill(PlayerSkills skill)
    {
        return skill switch
        {
            PlayerSkills.Health_2 => PlayerSkills.Health_1,
            _ => PlayerSkills.None
        };
    }

    private bool IsUnlockedForPlayer(PlayerSkills skill) {
        var dependentSkill = GetDependentSkill(skill);
        return dependentSkill == PlayerSkills.None || PlayerHasSkill(dependentSkill);
    }


    public bool PlayerHasSkill(PlayerSkills skill) {
        return _playerSkills.Contains(skill);
    }
    public int PlayerSkillsCount() {
        return _playerSkills.Count;
    }
    public int GetMaxLevel() {
        return _allSkills.Count;
    }
}
