using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter {
    // private bool _canShoot = true;
    // private Gun _gun;
    // Transform _hand;
    // Transform _firePoint;

    // public PlayerShooter(Gun gun, Transform hand, Transform firePoint) {
    //     _gun = gun;
    //     _hand = hand;
    //     _firePoint = firePoint;
    // }

    // public void Shoot() {
    //     // if (paused) return;
    //     // if (_gun == null) return;
    //     if (!_canShoot) return;
    //     _gun.Fire(_hand, _firePoint);
    //     EventManager.instance.TriggerEvent("OnShoot");
    //     OnUpdateAmmo?.Invoke(_gun.Ammo);
    //     _canShoot = false;
    //     Invoke("CanShootAgain", _gun.FireRate);
    // }

    // public void CanShootAgain() {
    //     EventManager.instance.TriggerEvent("OnCanShoot");
    //     _canShoot = true;
    // }

    // public void Reload() {
    //     if (_gun.Ammo == _gun.MaxAmmo) return;
    //     _gun.Ammo = _gun.MaxAmmo;
    //     OnUpdateAmmo?.Invoke(_gun.Ammo);
    //     reloadTimer = 0;

    // }

}
