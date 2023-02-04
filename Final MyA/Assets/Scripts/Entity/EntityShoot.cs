using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class EntityShoot : Entity {

    [SerializeField]
    public Gun gun;

    [SerializeField]
    protected GunsType _gunsType;

    [SerializeField]
    protected bool _canShoot;
    [SerializeField]
    protected Transform _firePoint;


    protected virtual void Shoot() {
        if (paused) return;
        if (gun == null) return;
        if (!_canShoot) return;
        gun.Fire(_hand, _firePoint);
        _canShoot = false;
        Invoke("CanShootAgain", gun.FireRate);
    }

    void CanShootAgain() {
        EventManager.instance.TriggerEvent("OnCanShoot");
        _canShoot = true;
    }

    protected virtual void Reload() {
        if (gun.Ammo == gun.MaxAmmo) return;
        gun.Ammo = gun.MaxAmmo;

    }
}
