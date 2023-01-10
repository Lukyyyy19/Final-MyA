using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : GunParts {

    [SerializeField]
    private int _addBullets = 10;
    [SerializeField]
    private float _addFirerate = -.22f;
    [SerializeField]
    private float _addReloadTime = -.25f;


    void Start() {
        gunPart = GunPart.Magazine;
    }

    public override void Attach(Gun gun) {
        gun.MaxAmmo += _addBullets;
        gun.FireRate += _addFirerate;
        gun.ReloadTime += _addReloadTime;
        gun.Ammo = gun.MaxAmmo;


    }
    public override void Deattach(Gun gun) {
        gun.MaxAmmo -= _addBullets;
        gun.FireRate -= _addFirerate;
        gun.ReloadTime -= _addReloadTime;
        gun.Ammo = gun.MaxAmmo;


    }


}
