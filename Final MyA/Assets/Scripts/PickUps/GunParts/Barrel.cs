using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : GunParts {

    [SerializeField]
    private float _addDamage;
    [SerializeField]
    private float _addFirerate;
    [SerializeField]
    private float _addSpread;
    [SerializeField]
    private int _bulletsPerShot;


    void Start() {
        gunPart = GunPart.Barrel;
        _addDamage = GunContainer.GetGun(gunsType).Damage;
        _addFirerate = GunContainer.GetGun(gunsType).FireRate;
        _addSpread = GunContainer.GetGun(gunsType).Spread;
        _bulletsPerShot = GunContainer.GetGun(gunsType).BulletsQty;

    }

    public override void Attach(Gun gun) {
        gun.Damage += _addDamage;
        gun.FireRate += _addFirerate;
        gun.Spread += _addSpread;
        gun.BulletsQty += _bulletsPerShot;


    }
    public override void Deattach(Gun gun) {
        gun.Damage -= _addDamage;
        gun.FireRate -= _addFirerate;
        gun.Spread -= _addSpread;
        gun.BulletsQty -= _bulletsPerShot;

    }


}
