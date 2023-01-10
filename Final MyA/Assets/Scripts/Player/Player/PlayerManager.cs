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






    GunsType currentGun;

    private HashSet<GunParts> upgrades = new HashSet<GunParts>();

    protected override void Awake() {
        base.Awake();
        instance = this;
        playerInputs = new PlayerInputs();
        gun = GunContainer.GetGun(_gunsType);
    }

    void Update() {
        playerInputs.ArtificialUpdate();
        _keyDirection.x = playerInputs.MovHor;
        _keyDirection.y = playerInputs.MovVer;
        if (currentGun != _gunsType) {
            VariableChangeHandler(_gunsType);
        }
        if (playerInputs.Fire) {
            Shoot();
        }
        if (playerInputs.Reload) {
            Invoke("Reload", gun.ReloadTime);
        }
        currentGun = gun.Name;


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
    }

    public void UpgradeGun(GunParts gunPart) {
        if (upgrades.Add(gunPart))
            gunPart.Attach(gun);
        gun.ClampValues();
        OnUpdateAmmo?.Invoke(gun.Ammo);



    }
    public void DowngradeGun(GunParts gunPart) {
        if (upgrades.Remove(gunPart)) {
            gunPart.Deattach(gun);
        }
        gun.ClampValues();
        OnUpdateAmmo?.Invoke(gun.Ammo);

    }
}
