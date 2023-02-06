using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesEnum;
using System;
public class TreeSkills {



    Dictionary<PlayerSkills, int> abilityPointsDic = new Dictionary<PlayerSkills, int>();
    List<PlayerSkills> unlockedSkills = new List<PlayerSkills>();
    public int abilityPoints;
    public Action<PlayerSkills> OnSkillUnlocked;
    public void Init() {
        abilityPointsDic.Add(PlayerSkills.Dash, 1);
        abilityPointsDic.Add(PlayerSkills.Dash1, 3);
        abilityPointsDic.Add(PlayerSkills.MaxHealth1, 1);
        abilityPointsDic.Add(PlayerSkills.MaxHealth2, 2);
        abilityPointsDic.Add(PlayerSkills.Damage, 1);
        abilityPointsDic.Add(PlayerSkills.Spread, 2);
        abilityPointsDic.Add(PlayerSkills.BulletQty, 3);
        abilityPointsDic.Add(PlayerSkills.FireRate, 1);
    }

    public PlayerSkills GetRandomAbility() {
        var ab = UnityEngine.Random.Range(0, 9);
        var ps = (PlayerSkills)ab;
        var canReturn = false;
        HashSet<PlayerSkills> randomSkills = new HashSet<PlayerSkills>();
        while (!canReturn) {
            var skillType = UnlockRequirement(ps);
            if (skillType != PlayerSkills.None) {
                if (IsUpgradeUnlocked(skillType) && randomSkills.Contains(ps)) {
                    randomSkills.Add(ps);
                    return ps;
                } else {
                    ab = UnityEngine.Random.Range(0, 9);
                    ps = (PlayerSkills)ab;
                }
            } else {
                randomSkills.Add(ps);
                return ps;
            }
        }
        return PlayerSkills.None;
    }


    public bool TryUnlockSkill(PlayerSkills skill) {
        var skillType = UnlockRequirement(skill);
        if (skillType != PlayerSkills.None) {
            if (IsUpgradeUnlocked(skillType)) {
                UnlockSkill(skill);
                return true;
            } else {
                return false;
            }
        } else {
            UnlockSkill(skill);
            return true;
        }
    }
    private void UnlockSkill(PlayerSkills skill) {
        if (abilityPoints >= abilityPointsDic[skill] && !IsUpgradeUnlocked(skill)) {
            abilityPoints -= abilityPointsDic[skill];
            unlockedSkills.Add(skill);
            OnSkillUnlocked?.Invoke(skill);
            Debug.Log($"Hablilidad desbloqueda: {skill}");
        } else {
            Debug.Log($"No tienes los puntos necesarios para desbloquear {skill} o ya esta desbloqueda");
        }
    }

    PlayerSkills UnlockRequirement(PlayerSkills skill) {
        switch (skill) {
            case PlayerSkills.MaxHealth2: return PlayerSkills.MaxHealth1;
            case PlayerSkills.Dash1: return PlayerSkills.Dash;
        }
        return PlayerSkills.None;
    }


    public bool IsUpgradeUnlocked(PlayerSkills skill) {
        return unlockedSkills.Contains(skill);
    }
}
