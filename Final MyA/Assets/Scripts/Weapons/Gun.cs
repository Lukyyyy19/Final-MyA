using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class Gun {

    private float _fireRate;
    private string _bulletType;
    private int _bulletsQty;
    private int _ammo;
    private int _maxAmmo;
    private float _reloadTime;
    private GunsType _name;
    private float _damage;
    private float _spread;

    public Gun(float fireRate, string bulletType, int maxAmmo, float reloadTime, float damage, float spread, GunsType name, int bulletsQty) {
        _fireRate = fireRate;
        _bulletType = bulletType;
        _maxAmmo = maxAmmo;
        _ammo = maxAmmo;
        _reloadTime = reloadTime;
        _name = name;
        _damage = damage;
        _spread = spread;
        _bulletsQty = bulletsQty;
    }

    public float FireRate { get => _fireRate; }
    public string BulletType { get => _bulletType; }
    public int Ammo { get => _ammo; set => _ammo = value; }
    public int MaxAmmo { get => _maxAmmo; }
    public float ReloadTime { get => _reloadTime; }
    public GunsType Name { get => _name; }
    public float Damage { get => _damage; }
    public float Spread { get => _spread; }
    public int BulletsQty { get => _bulletsQty; }
}
