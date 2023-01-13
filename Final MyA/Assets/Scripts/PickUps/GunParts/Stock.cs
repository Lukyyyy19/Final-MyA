using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class Stock : GunParts {
    [SerializeField]
    private float _addFirerate;
    [SerializeField]
    private float _addSpread;

    void Start() {
        gunPart = GunPart.Stock;
        InitialStats();
    }

    protected override void InitialStats() {
        base.InitialStats();
        _addFirerate = GunContainer.GetGun(gunsType).FireRate;
        _addSpread = GunContainer.GetGun(gunsType).Spread;
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
