using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesEnum;
using System;
[Serializable]
public class TreeSkills {
    [SerializeField]
    List<PlayerSkills> allSkills = new List<PlayerSkills>();
    [SerializeField]
    List<PlayerSkills> unlockedSkills = new List<PlayerSkills>();
    // [SerializeField]
    // List<PlayerSkills> tempAllSkills = new List<PlayerSkills>();
    public Action<PlayerSkills> OnSkillUnlocked;
    public void Init() {
        allSkills.Add(PlayerSkills.Dash);
        allSkills.Add(PlayerSkills.Dash1);
        allSkills.Add(PlayerSkills.MaxHealth1);
        allSkills.Add(PlayerSkills.MaxHealth2);
        allSkills.Add(PlayerSkills.BulletQty);
        allSkills.Add(PlayerSkills.FireRate);
        allSkills.Add(PlayerSkills.Damage);
    }

    public List<PlayerSkills> GetRandomAbility() {
        int numberOfRepetitions = 3;
        List<PlayerSkills> randomSkills = new List<PlayerSkills>();
        List<PlayerSkills> tempAllSkills = new List<PlayerSkills>(allSkills);
        if (unlockedSkills.Count > allSkills.Count - 4) {
            numberOfRepetitions = allSkills.Count - unlockedSkills.Count;
            Debug.Log("Reduciendo el numero de repeticiones en: " + numberOfRepetitions);
        }
        for (int i = 0; i < numberOfRepetitions; i++) {
            Debug.Log("La cantidad de items en la lista temp es " + tempAllSkills.Count);
            int ab = UnityEngine.Random.Range(0, tempAllSkills.Count);
            var ps = tempAllSkills[ab];

            var skillTypeRequirerment = UnlockRequirement(ps);
            if (!IsUpgradeUnlocked(ps)) {
                if (skillTypeRequirerment != PlayerSkills.None) {
                    if (IsUpgradeUnlocked(skillTypeRequirerment)) {
                        tempAllSkills.Remove(ps);
                        Debug.Log($"Aniadiendo la skill que requiere {skillTypeRequirerment} y es {ps}");
                        randomSkills.Add(ps);
                    } else {
                        Debug.Log("Eligiendo otra habilidad debido a que ya fue elegida");
                        ab = UnityEngine.Random.Range(0, tempAllSkills.Count);
                        ps = tempAllSkills[ab];
                        numberOfRepetitions++;
                        Debug.Log("La nueva habilidad es " + ps);
                    }
                } else {
                    tempAllSkills.Remove(ps);
                    Debug.Log("Aniadiendo nueva skill que es " + ps);
                    randomSkills.Add(ps);
                }
            } else {
                // ab = UnityEngine.Random.Range(0, tempAllSkills.Count);
                // ps = tempAllSkills[ab];
                numberOfRepetitions++;
            }
        }
        Debug.Log("La lista contiene " + randomSkills.Count);
        return randomSkills;
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
        if (!IsUpgradeUnlocked(skill)) {
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
    public int SkillsUnlockedCount() {
        return unlockedSkills.Count;
    }
}
