using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBody : Magazine {

    void Start() {
        _Bullets = GunContainer.instance.shotgun.MaxAmmo;
        _Firerate = GunContainer.instance.shotgun.FireRate;
        _ReloadTime = GunContainer.instance.shotgun.ReloadTime;
        _bulletType = GunContainer.instance.shotgun.BulletType;
    }


}
