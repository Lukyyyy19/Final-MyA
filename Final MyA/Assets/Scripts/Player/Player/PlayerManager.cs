using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class PlayerManager : Entity {

    public static PlayerManager instance;
    public PlayerInputs playerInputs;

    private Vector2 _keyDirection;

    public delegate void UpdateAmmoDelegate(int bullet);
    public event UpdateAmmoDelegate OnUpdateAmmo;

    public GunStats _gunStats;
    protected override void Awake() {
        base.Awake();
        instance = this;
        playerInputs = new PlayerInputs();
        _gunStats = GetComponentInChildren<GunStats>();
        // gun = _gunStats.gun;
        gun = GunContainer.GetGun(_gunsType);
    }

    void Update() {
        playerInputs.ArtificialUpdate();
        _keyDirection.x = playerInputs.MovHor;
        _keyDirection.y = playerInputs.MovVer;

        if (playerInputs.Fire) {
            Shoot();
        }
        if (playerInputs.Reload) {
            Invoke("Reload", gun.ReloadTime);
        }
    }

    protected override void Shoot() {
        base.Shoot();
        OnUpdateAmmo?.Invoke(gun.Ammo);
    }

    protected override void Reload() {
        base.Reload();
        OnUpdateAmmo?.Invoke(gun.Ammo);

    }

    private void FixedUpdate() {
        Move(_keyDirection);
    }

    private void VariableChangeHandler(GunsType newGun) {
        gun = GunContainer.GetGun(newGun);
        OnUpdateAmmo?.Invoke(gun.Ammo);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        IPickeupable pickeupable;
        if (!other.TryGetComponent<IPickeupable>(out pickeupable)) return;
        pickeupable.OnPickUp();
        OnUpdateAmmo?.Invoke(gun.Ammo);

        // GunParts _gunParts;
        // if (other.TryGetComponent<GunParts>(out _gunParts)) {
        //     _gunStats.UpgradeGun(_gunParts._gunPartSo.);
        // }
    }


    // void AsignPlayerGun() {
    //     gun = GunContainer.GetGun(_gunsType);
    // }
    // private void OnDisable() {
    //     GunContainer.instance.OnCreate -= AsignPlayerGun;
    // }
    // private void OnEnable() {
    //     GunContainer.instance.OnCreate += AsignPlayerGun;

    // }
}
