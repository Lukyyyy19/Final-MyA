using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
using System;
public class Gun : ICloneable {

    private float _fireRate;
    private string _bulletType;
    private int _bulletsQty;
    private int _ammo;
    private int _maxAmmo;
    private float _reloadTime;
    private GunsType _name;
    private float _damage;
    private float _spread;
    public Action<GunsType, Gun> notify;

    // private MiddlePart _middlePart;
    // private Stock _stockPart;
    // private Cannon _cannonPart;

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

    // public Gun(MiddlePart middlePart, Stock stockPart, Cannon cannonPart, GunsType gunType) {
    //     _middlePart = middlePart;
    //     _stockPart = stockPart;
    //     _cannonPart = cannonPart;
    //     _name = gunType;
    // }

    public void Configure(Action<GunsType, Gun> notify) {
        //ScreenManager.instance.AddPausable(this);
        this.notify = notify;
    }

    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public string BulletType { get => _bulletType; set => _bulletType = value; }
    public int BulletsQty { get => _bulletsQty; set => _bulletsQty = value; }
    public int Ammo { get => _ammo; set => _ammo = value; }
    public int MaxAmmo { get => _maxAmmo; set => _maxAmmo = value; }
    public float ReloadTime { get => _reloadTime; set => _reloadTime = value; }
    public GunsType Name { get => _name; set => _name = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public float Spread { get => _spread; set => _spread = value; }

    public void ClampValues() {
        _reloadTime = Mathf.Clamp(_reloadTime, .05f, 5);
        _spread = Mathf.Clamp(_spread, 0, 2);
        _damage = Mathf.Clamp(_damage, 1, 99);
        _fireRate = Mathf.Clamp(_fireRate, .05f, 5);
        _maxAmmo = Mathf.Clamp(_maxAmmo, 1, 999);

    }

    public object Clone() {
        return this.MemberwiseClone();
    }
}
