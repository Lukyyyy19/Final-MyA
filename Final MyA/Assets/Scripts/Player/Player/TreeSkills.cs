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
    [SerializeField] int numberOfRepetitions = 3;
    // [SerializeField]
    // List<PlayerSkills> tempAllSkills = new List<PlayerSkills>();
    public Action<PlayerSkills> OnSkillUnlocked;
    public void Init() {
        allSkills.Add(PlayerSkills.Dash);
        allSkills.Add(PlayerSkills.MaxHealth1);
        allSkills.Add(PlayerSkills.MaxHealth2);
        allSkills.Add(PlayerSkills.BulletQty);
        allSkills.Add(PlayerSkills.FireRate);
        allSkills.Add(PlayerSkills.Damage);
    }

    public List<PlayerSkills> GetRandomAbility() {
        List<PlayerSkills> randomSkills = new List<PlayerSkills>();
        var allowedSkills = allSkills.Where(skill => !PlayerHasSkill(skill) && IsUnlockedForPlayer(skill)).ToList();
        // if (unlockedSkills.Count > allSkills.Count - 4) {
        //     numberOfRepetitions = allSkills.Count - unlockedSkills.Count;
        //     Debug.Log("Reduciendo el numero de repeticiones en: " + numberOfRepetitions);
        // }
        int numberOfRepetitions = allSkills.Count - playerSkills.Count > 3 ? 3 : allSkills.Count - playerSkills.Count;
        while (randomSkills.Count < numberOfRepetitions) {
            //Debug.Log("La cantidad de items en la lista temp es " + alllSkills.Count);
            int ab = UnityEngine.Random.Range(0, allowedSkills.Count);
            var ps = allowedSkills[ab];
            allowedSkills.Remove(ps);
            Debug.Log("Aniadiendo nueva skill que es " + ps);
            randomSkills.Add(ps);
            // var skillTypeRequirerment = GetDependentSkill(ps);
            // if (skillTypeRequirerment != PlayerSkills.None) {
            //     if (IsUpgradeUnlocked(skillTypeRequirerment)) {
            //         allowedSkills.Remove(ps);
            //         Debug.Log($"Aniadiendo la skill que requiere {skillTypeRequirerment} y es {ps}");
            //         randomSkills.Add(ps);
            //     } else {
            //         Debug.Log("Eligiendo otra habilidad debido a que ya fue elegida");
            //         ab = UnityEngine.Random.Range(0, allowedSkills.Count);
            //         ps = allowedSkills[ab];
            //         // numberOfRepetitions++;
            //         Debug.Log("La nueva habilidad es " + ps);
            //     }
            // } else {


            //else {
            //     // ab = UnityEngine.Random.Range(0, tempAllSkills.Count);
            //     // ps = tempAllSkills[ab];
            //     //  numberOfRepetitions++;
            // }
        }
        Debug.Log("La lista contiene " + randomSkills.Count);
        return randomSkills;
    }


    public bool TryUnlockSkill(PlayerSkills skill) {
        var skillType = GetDependentSkill(skill);
        if (skillType != PlayerSkills.None) {
            if (PlayerHasSkill(skillType)) {
                AddToPlayerSkills(skill);
                return true;
            } else {
                return false;
            }
        } else {
            AddToPlayerSkills(skill);
            return true;
        }
    }
    private void AddToPlayerSkills(PlayerSkills skill) {
        if (!PlayerHasSkill(skill)) {
            playerSkills.Add(skill);
            OnSkillUnlocked?.Invoke(skill);
            Debug.Log($"Hablilidad desbloqueda: {skill}");
        } else {
            Debug.Log($"No tienes los puntos necesarios para desbloquear {skill} o ya esta desbloqueda");
        }
    }

    PlayerSkills GetDependentSkill(PlayerSkills skill) {
        switch (skill) {
            case PlayerSkills.MaxHealth2: return PlayerSkills.MaxHealth1;
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
}
