using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPart : GunParts {
    [SerializeField]
    private StockSO _stockSO;
    public override void OnPickUp() {
        PlayerManager.instance._gunStats.UpgradeGun(_stockSO);
        Destroy(gameObject);
    }
}
