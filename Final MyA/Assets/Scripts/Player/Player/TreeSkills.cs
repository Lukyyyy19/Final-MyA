using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesEnum;
using System;
using System.Linq;
[Serializable]


public class TreeSkills {
    [SerializeField]
    List<PlayerSkills> allSkills = new List<PlayerSkills>();
    [SerializeField]
    List<PlayerSkills> playerSkills = new List<PlayerSkills>();
    [SerializeField]
    List<PlayerSkills> allowedSkills = new List<PlayerSkills>();
    [SerializeField]
    List<PlayerSkills> randomSkills = new List<PlayerSkills>();
    [SerializeField]
    int numberOfRepetitions;
    public Action<PlayerSkills> OnSkillUnlocked;
    public void Init() {
        allSkills.Add(PlayerSkills.Dash);
        allSkills.Add(PlayerSkills.MaxHealth1);
        allSkills.Add(PlayerSkills.MaxHealth2);
        allSkills.Add(PlayerSkills.MaxHealth3);
        allSkills.Add(PlayerSkills.BulletQty);
        allSkills.Add(PlayerSkills.FireRate);
        allSkills.Add(PlayerSkills.Damage);
    }

    public List<PlayerSkills> GetRandomAbility() {
        randomSkills.Clear();
        allowedSkills = allSkills.Where(skill => !PlayerHasSkill(skill) && IsUnlockedForPlayer(skill)).ToList();

        numberOfRepetitions = allowedSkills.Count > 3 ? 3 : allowedSkills.Count;
        while (randomSkills.Count < numberOfRepetitions) {
            int ab = UnityEngine.Random.Range(0, allowedSkills.Count);
            var ps = allowedSkills[ab];
            allowedSkills.Remove(ps);
            Debug.Log("Aniadiendo nueva skill que es " + ps);
            randomSkills.Add(ps);
        }
        Debug.Log($"La lista contiene {randomSkills.Count}: {randomSkills[0]}");
        return randomSkills;
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
    private void AddToPlayerSkills(PlayerSkills skill) {
        if (!PlayerHasSkill(skill)) {
            playerSkills.Add(skill);
            OnSkillUnlocked?.Invoke(skill);
            Debug.Log($"Hablilidad desbloqueda: {skill}");
        }
        else {
            Debug.Log($"No tienes los puntos necesarios para desbloquear {skill} o ya esta desbloqueda");
        }
    }

    PlayerSkills GetDependentSkill(PlayerSkills skill) {
        switch (skill) {
            case PlayerSkills.MaxHealth2: return PlayerSkills.MaxHealth1;
            case PlayerSkills.MaxHealth3: return PlayerSkills.MaxHealth2;
        }
        return PlayerSkills.None;
    }

    bool IsUnlockedForPlayer(PlayerSkills skill) {
        PlayerSkills dependentSkill = GetDependentSkill(skill);
        return dependentSkill == PlayerSkills.None || PlayerHasSkill(dependentSkill);
    }


    public bool PlayerHasSkill(PlayerSkills skill) {
        return playerSkills.Contains(skill);
    }
    public int PlayerSkillsCount() {
        return playerSkills.Count;
    }
    public int GetMaxLevel() {
        return allSkills.Count;
    }
}
