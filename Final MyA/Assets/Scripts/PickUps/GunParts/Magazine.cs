using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class Magazine : GunParts {

    [SerializeField]
    protected int _Bullets;
    [SerializeField]
    protected string _bulletType;
    [SerializeField]
    protected float _Firerate;
    [SerializeField]
    protected float _ReloadTime;


    void Start() {
        gunPart = GunPart.Magazine;

    }

    protected override void InitialStats() {
        base.InitialStats();
        _Bullets = GunContainer.GetGun(gunsType).MaxAmmo;
        _Firerate = GunContainer.GetGun(gunsType).FireRate;
        _ReloadTime = GunContainer.GetGun(gunsType).ReloadTime;
        _bulletType = GunContainer.GetGun(gunsType).BulletType;
    }
    public override void Attach(Gun gun) {
        gun.MaxAmmo += _Bullets;
        gun.FireRate += _Firerate;
        gun.ReloadTime += _ReloadTime;
        gun.Ammo = gun.MaxAmmo;


    }
    public override void Deattach(Gun gun) {
        gun.MaxAmmo -= _Bullets;
        gun.FireRate -= _Firerate;
        gun.ReloadTime -= _ReloadTime;
        gun.Ammo = gun.MaxAmmo;


    }


}
