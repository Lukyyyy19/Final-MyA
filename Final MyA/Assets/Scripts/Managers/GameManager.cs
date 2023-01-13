using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : InstanceClass<GameManager> {

    public GunsPool _gunPool;

    void CreateGunsForEnemy() {
        _gunPool.IntantiateGuns(GunsEnum.GunsType.Pistol, 3);
    }
    private void OnDisable() {
        GunContainer.OnCreate -= CreateGunsForEnemy;
    }
    private void OnEnable() {
        GunContainer.OnCreate += CreateGunsForEnemy;

    }
}
