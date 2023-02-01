using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesEnum;
using System;
public class TreeSkills {



    Dictionary<PlayerSkills, int> abilityPointsDic = new Dictionary<PlayerSkills, int>();
    List<PlayerSkills> unlockedSkills = new List<PlayerSkills>();
    List<GunUpgrades> unlockedGunUpgrades = new List<GunUpgrades>();
    public int abilityPoints;
    public Action<PlayerSkills> OnSkillUnlocked;
    public void Init() {
        abilityPointsDic.Add(PlayerSkills.Dash, 1);
        abilityPointsDic.Add(PlayerSkills.MaxHealth1, 1);
        abilityPointsDic.Add(PlayerSkills.MaxHealth2, 1);
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
        if (abilityPoints >= abilityPointsDic[skill]) {
            abilityPoints -= abilityPointsDic[skill];
            unlockedSkills.Add(skill);
            OnSkillUnlocked?.Invoke(skill);
            Debug.Log($"Hablilidad desbloqueda: {skill}");
        } else {
            Debug.Log($"No tienes los puntos necesarios para desbloquear {skill}");
        }
    }

    PlayerSkills UnlockRequirement(PlayerSkills skill) {
        switch (skill) {
            case PlayerSkills.MaxHealth2: return PlayerSkills.MaxHealth1;
        }
        return PlayerSkills.None;
    }


    public bool IsUpgradeUnlocked(PlayerSkills skill) {
        return unlockedSkills.Contains(skill);
    }
    public bool IsUpgradeUnlocked(GunUpgrades upgrade) {
        return unlockedGunUpgrades.Contains(upgrade);
    }
}
