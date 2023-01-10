using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : GunParts {

    private float _addDamage = 1f;
    private float _addFirerate = -.22f;
    private float _addSpread = .5f;
    void Start() {
        gunPart = GunPart.Barrel;
    }


    public override void Upgrade() {
        PlayerManager.instance.gun.Damage += _addDamage;
        PlayerManager.instance.gun.FireRate += _addFirerate;
        PlayerManager.instance.gun.Spread += _addSpread;


    }
    public override void Downgrade() {
        PlayerManager.instance.gun.Damage -= _addDamage;
        PlayerManager.instance.gun.FireRate -= _addFirerate;
        PlayerManager.instance.gun.Spread -= _addSpread;

    }


}
