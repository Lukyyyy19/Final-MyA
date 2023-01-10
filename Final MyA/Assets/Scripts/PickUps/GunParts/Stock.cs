using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : GunParts {
    [SerializeField]
    private float _addFirerate = -.12f;
    [SerializeField]
    private float _addSpread = -.3f;

    void Start() {
        gunPart = GunPart.Barrel;
    }

    public override void Attach(Gun gun) {
        gun.FireRate += _addFirerate;
        gun.Spread += _addSpread;


    }
    public override void Deattach(Gun gun) {
        gun.FireRate -= _addFirerate;
        gun.Spread -= _addSpread;

    }
}
