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
    protected bool isPaused;

    [SerializeField]
    private TrailRenderer _trail;

    [SerializeField]
    private bool _invencible;

    protected override void Awake() {
        base.Awake();
        instance = this;
        playerInputs = new PlayerInputs();
        _gunStats = GetComponentInChildren<GunStats>();
        // gun = _gunStats.gun;

    }

    protected override void Start() {
        AsignPlayerGun();
        base.Start();
        // GunContainer.instance.Init();
    }

    void Update() {
        if (!isPaused)
            playerInputs.ArtificialUpdate();
        _keyDirection.x = playerInputs.MovHor;
        _keyDirection.y = playerInputs.MovVer;

        if (playerInputs.Fire) {
            Shoot();
        }
        if (playerInputs.Reload) {
            Invoke("Reload", gun.ReloadTime);
        }
        if (playerInputs.Dash && _canDash) {
            _startDash = true;
        }
        if (_startDash) {
            _trail.enabled = true;
            _invencible = true;
            Attack(_keyDirection);
        }
        if (!_isDashing) {
            _invencible = false;
            _trail.enabled = false;
        }
    }

    private void FixedUpdate() {
        if (!_isDashing)
            Move(_keyDirection);

    }

    protected override void Shoot() {
        base.Shoot();
        OnUpdateAmmo?.Invoke(gun.Ammo);
    }

    protected override void Reload() {
        base.Reload();
        OnUpdateAmmo?.Invoke(gun.Ammo);

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


    void AsignPlayerGun() {
        Debug.Log("Creando arma player");
        gun = GunContainer.GetGun(_gunsType);
        _gunStats.Init();
    }
    protected override void OnDisable() {
        base.OnDisable();
        EventManager.instance.RemoveAction("CreateGuns", AsignPlayerGun);
    }
    public override void TakeDamage(int damage) {
        if (!_invencible)
            base.TakeDamage(damage);
    }

    public override void Pause() {
        base.Pause();
        isPaused = true;
    }
    public override void Resume() {
        base.Resume();
        isPaused = false;
    }
}
