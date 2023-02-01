using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
using UpgradesEnum;
public class PlayerManager : Entity {

    public static PlayerManager instance;
    public PlayerInputs playerInputs;
    public PlayerAnimation playerAnimation;


    private Vector2 _keyDirection;

    public delegate void UpdateAmmoDelegate(int bullet);
    public event UpdateAmmoDelegate OnUpdateAmmo;

    public GunStats _gunStats;
    protected bool isPaused;

    [SerializeField]
    private TrailRenderer _trail;

    private TreeSkills _treeSkills;


    [SerializeField]
    private bool _invencible;

    public float reloadTimer;
    public float reloadTimerStart;

    private int enemyDeadCounter;
    [SerializeField]
    private int nextLevel = 3;

    public TreeSkills TreeSkills { get => _treeSkills; }

    protected override void Awake() {
        base.Awake();
        instance = this;
        playerInputs = new PlayerInputs();
        _gunStats = GetComponentInChildren<GunStats>();
        playerAnimation = GetComponent<PlayerAnimation>();
        _treeSkills = new TreeSkills();
        _treeSkills.Init();
        _treeSkills.OnSkillUnlocked += OnSkillUnlocked;
        // gun = _gunStats.gun;

    }

    protected override void Start() {
        AsignPlayerGun();
        base.Start();
        // GunContainer.instance.Init();
    }

    void Update() {
        if (isPaused) return;
        playerInputs.ArtificialUpdate();
        _keyDirection.x = playerInputs.MovHor;
        _keyDirection.y = playerInputs.MovVer;

        if (gun.Ammo < 1) {
            reloadTimer += Time.deltaTime;
            Invoke("Reload", gun.ReloadTime);
        }
        if (reloadTimer >= reloadTimerStart) {
            reloadTimer = reloadTimerStart;
            Debug.Log("Arma Cargada tiempo");
        }
        if (playerInputs.Fire) {
            Shoot();
        }
        if (playerInputs.Reload) {
            Invoke("Reload", gun.ReloadTime);
        }
        if (playerInputs.Dash && _canDash && TreeSkills.IsUpgradeUnlocked(PlayerSkills.Dash)) {
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

        playerAnimation.SetBoolParam("IsMoving", isMoving);
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
        reloadTimer = 0;
        Debug.Log("Arma cargada metodo");
    }

    public void UpdateReloadTimer(float time) {
        reloadTimerStart = time;
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
    }

    public void EnemyKill() {
        StartCoroutine("DieStopTime");
        enemyDeadCounter++;
        if (enemyDeadCounter >= nextLevel) {
            LevelUp();
        }
    }

    private void LevelUp() {
        _treeSkills.abilityPoints++;
        Debug.Log("nivel nuevo");
        nextLevel = Mathf.CeilToInt(nextLevel * 1.5f);
    }

    protected IEnumerator DieStopTime() {
        ScreenManager.instance.Pause();
        yield return new WaitForSeconds(.07f);
        ScreenManager.instance.Resume();
    }


    void AsignPlayerGun() {
        Debug.Log("Creando arma player");
        gun = GunContainer.GetGun(_gunsType);
        reloadTimerStart = gun.ReloadTime;
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

    private void OnSkillUnlocked(PlayerSkills skill) {
        switch (skill) {
            case PlayerSkills.MaxHealth1:
                _maxHealth = 150;
                break;
            case PlayerSkills.MaxHealth2:
                _maxHealth = 200;
                break;
        }
    }

    public void FlipPlayer(float xValue) {
        Vector3 localScale = transform.localScale;
        if (xValue < 0) localScale.x = Mathf.Abs(localScale.x) * -1;
        else if (xValue > 0) localScale.x = Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }
}
