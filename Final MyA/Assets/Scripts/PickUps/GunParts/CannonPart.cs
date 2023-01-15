using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPart : GunParts {
    [SerializeField]
    private CannonSO _cannonSO;
    public override void OnPickUp() {
        PlayerManager.instance._gunStats.UpgradeGun(_cannonSO);
        Destroy(gameObject);
    }
}
