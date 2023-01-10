using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class PlayerManager : Entity {

    public PlayerInputs playerInputs;

    private Vector2 _keyDirection;

    public delegate void ShootDelegate(int bullet);
    public event ShootDelegate OnShoot;

    public delegate void OnVariableChangeDelegate(GunsType newGun);
    public event OnVariableChangeDelegate OnVariableChange;

    GunsType currentGun;
    public GunsType gunsType {
        set {
            if (_gunsType == value) return;
            _gunsType = value;
        }
    }



    protected override void Awake() {
        base.Awake();
        playerInputs = new PlayerInputs();
        gun = GunContainer.GetGun(_gunsType);
    }

    void Update() {
        playerInputs.ArtificialUpdate();
        _keyDirection.x = playerInputs.MovHor;
        _keyDirection.y = playerInputs.MovVer;
        Debug.Log(gun.Name);
        if (currentGun != _gunsType) {
            OnVariableChange?.Invoke(_gunsType);
        }
        if (playerInputs.Fire) {
            Shoot();
        }
        currentGun = gun.Name;
    }

    protected override void Shoot() {
        base.Shoot();
        OnShoot?.Invoke(gun.Ammo);
    }

    private void FixedUpdate() {
        Move(_keyDirection);
    }

    private void OnEnable() {
        OnVariableChange = VariableChangeHandler;
    }
    private void OnDisable() {
        OnVariableChange -= VariableChangeHandler;
    }
    private void VariableChangeHandler(GunsType newGun) {
        gun = GunContainer.GetGun(newGun);
        OnShoot?.Invoke(gun.Ammo);

    }


}
