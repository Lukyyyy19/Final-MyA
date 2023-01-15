using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePart : GunParts {
    [SerializeField]
    private MiddlePartSO _middlePartSO;
    public override void OnPickUp() {
        PlayerManager.instance._gunStats.UpgradeGun(_middlePartSO);
        Destroy(gameObject);
    }
}
