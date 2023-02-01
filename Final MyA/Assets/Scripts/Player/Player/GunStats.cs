using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class GunStats : MonoBehaviour {

    public HashSet<GunPart> upgrades = new HashSet<GunPart>();
    [SerializeField]
    public Gun gun;

    [SerializeField]
    private float _initialDamage;

    [SerializeField]
    private float _initialFirerate;

    [SerializeField]
    private float _initialSpread;

    [SerializeField]
    private int _initialbulletsPerShot;

    [SerializeField]
    protected int _initialBullets;

    [SerializeField]
    protected string _initialbulletType;

    [SerializeField]
    protected float _initialReloadTime;

    public MiddlePartSO _currentMiddlePart;
    public CannonSO _currentCannon;
    public StockSO _currentStock;

    public GunPart gunPicked;


    public void Init() {
        gun = PlayerManager.instance.gun;
        _initialBullets = gun.MaxAmmo;
        _initialbulletsPerShot = gun.BulletsQty;
        _initialbulletType = gun.BulletType;
        _initialDamage = gun.Damage;
        _initialFirerate = gun.FireRate;
        _initialReloadTime = gun.ReloadTime;
        _initialSpread = gun.Spread;
    }

    public void UpgradeGun(StockSO gunPart) {
        if (upgrades.Add(gunPart.gunpart)) {
            _currentStock = gunPart;
            gun.FireRate += gunPart.fireRate;
            gun.Spread += gunPart.spread;
            gun.ClampValues();
        } else {
            DowngradeGun(_currentStock);
            UpgradeGun(gunPart);
        }
        // OnUpdateAmmo?.Invoke(gun.Ammo);
    }
    public void UpgradeGun(CannonSO gunPart) {
        if (upgrades.Add(gunPart.gunpart)) {
            _currentCannon = gunPart;
            gun.Spread += gunPart.spread;
            gun.BulletsQty = gunPart.bulletsPerShot;
            gun.FireRate += gunPart.fireRate;
            gun.ClampValues();
        } else {
            DowngradeGun(_currentCannon);
            UpgradeGun(gunPart);
        }
        // OnUpdateAmmo?.Invoke(gun.Ammo);
    }
    public void UpgradeGun(MiddlePartSO gunPart) {
        if (upgrades.Add(gunPart.gunpart)) {
            _currentMiddlePart = gunPart;
            gun.MaxAmmo = gunPart.maxAmmo;
            gun.Damage += gunPart.damage;
            gun.BulletType = gunPart.bulletType;
            gun.ReloadTime += gunPart.reloadTime;
            gun.Ammo = gun.MaxAmmo;
            gun.ClampValues();
        } else {
            DowngradeGun(_currentMiddlePart);
            UpgradeGun(gunPart);
        }
        // OnUpdateAmmo?.Invoke(gun.Ammo);
    }
    // public void DowngradeGun(GunParts gunPart) {
    //     if (upgrades.Remove(gunPart.gunPart)) {
    //         Debug.Log("Removiendo");
    //         gunPart.Deattach(gun);
    //     }
    //     gun.ClampValues();
    //     //OnUpdateAmmo?.Invoke(gun.Ammo);

    // }

    public void DowngradeGun(StockSO gunPart) {
        if (upgrades.Remove(gunPart.gunpart)) {

            gun.FireRate -= gunPart.fireRate;
            gun.Spread -= gunPart.spread;
            gun.ClampValues();
        }
        // _currentStock = gunPart;
        // OnUpdateAmmo?.Invoke(gun.Ammo);
    }
    public void DowngradeGun(CannonSO gunPart) {
        if (upgrades.Remove(gunPart.gunpart)) {

            gun.Spread -= gunPart.spread;
            gun.BulletsQty = _initialbulletsPerShot;
            gun.FireRate -= gunPart.fireRate;
            gun.ClampValues();
        }
        // OnUpdateAmmo?.Invoke(gun.Ammo);
    }
    public void DowngradeGun(MiddlePartSO gunPart) {
        if (upgrades.Remove(gunPart.gunpart)) {
            Debug.Log("Removiendo");

            gun.MaxAmmo = _initialBullets;
            gun.Damage -= gunPart.damage;
            gun.BulletType = _initialbulletType;
            gun.ReloadTime -= gunPart.reloadTime;
            gun.Ammo = gun.MaxAmmo;
            gun.ClampValues();
        }
        // _currentMiddlePart = gunPart;
        // OnUpdateAmmo?.Invoke(gun.Ammo);
    }
}
