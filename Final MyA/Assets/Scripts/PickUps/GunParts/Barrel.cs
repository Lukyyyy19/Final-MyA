using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : GunParts {

    [SerializeField]
    private float _addDamage = 1f;
    [SerializeField]
    private float _addFirerate = -.22f;
    [SerializeField]
    private float _addSpread = .5f;

    void Start() {
        gunPart = GunPart.Barrel;
    }

    public override void Attach(Gun gun) {
        gun.Damage += _addDamage;
        gun.FireRate += _addFirerate;
        gun.Spread += _addSpread;


    }
    public override void Deattach(Gun gun) {
        gun.Damage -= _addDamage;
        gun.FireRate -= _addFirerate;
        gun.Spread -= _addSpread;

    }


}
