using System.Collections;
using UnityEngine;
using GunsEnum;
using UpgradesEnum;
public class PlayerManager : Entity, IHaveGun {

    public delegate void UpdateAmmoDelegate(int bullet);
    public event UpdateAmmoDelegate OnUpdateAmmo;

    protected bool isPaused;
    private Vector2 _keyDirection;

    [SerializeField]
    newUITreeSkill _uiTreeSkill;


    [Header("Components")]
    [SerializeField]
    private TrailRenderer _trail;
    [SerializeField]
    private ParticleSystem _dashParticle;
    [SerializeField]
    private BoxCollider2D _wallCollider;
    private GunStats gunStats;
    public static PlayerManager instance;
    public PlayerInputs playerInputs;
    public PlayerAnimation playerAnimation;
    [SerializeField]
    private TreeSkills _treeSkills;
    public UpdatePlayerVisuals playerVisuals;


    [HideInInspector]
    public float reloadTimer;
    [HideInInspector]
    public float reloadTimerStart;

    [Header("Level Count")]
    [SerializeField]
    private int enemyDeadCounter;
    [SerializeField]
    private int nextLevel = 3;
    [SerializeField]
    private int _currentLevel;
    private int _maxLevel;


    [Header("Raycasting")]
    [SerializeField]
    private LayerMask _layerWall;
    [SerializeField]
    private float radius;

    [Header("Dash")]
    [SerializeField]
    private bool _invencible;
    [SerializeField]
    protected bool _canDash = true;
    protected bool _isDashing;
    [SerializeField]
    protected float dashSpeed;
    [SerializeField]
    protected bool _startDash;
    [SerializeField]
    protected float dashCooldown;
    [SerializeField]
    private float _startTimeFloat = .1f;
    [SerializeField]
    private float _timer;
    private bool _startTime;


    [SerializeField]
    private GunsType _gunsType;

    [SerializeField]
    private bool _canShoot;
    [SerializeField]
    private Transform _firePoint;


    public TreeSkills TreeSkills { get => _treeSkills; }
    public GunStats GunStats { get => gunStats; set => gunStats = value; }

    protected override void Awake() {
        base.Awake();
        instance = this;
        playerInputs = new PlayerInputs();
        playerVisuals = new UpdatePlayerVisuals(sr, transform);
        gunStats = new GunStats();
        _timer = _startTimeFloat;
        playerAnimation = GetComponent<PlayerAnimation>();
        _treeSkills = new TreeSkills();
        _treeSkills.Init();
        _treeSkills.OnSkillUnlocked += OnSkillUnlocked;
        _maxLevel = _treeSkills.GetMaxLevel();
    }

    protected override void Start() {
        AsignGun();
        base.Start();
    }

    void Update() {
        playerInputs.ArtificialUpdate();
        if (isPaused) return;
        _keyDirection.x = playerInputs.MovHor;
        _keyDirection.y = playerInputs.MovVer;


        if (gunStats.gun.Ammo < 1) {
            reloadTimer += Time.deltaTime;
            Invoke("Reload", gunStats.gun.ReloadTime);
        }
        if (reloadTimer >= reloadTimerStart) {
            reloadTimer = reloadTimerStart;
        }
        if (playerInputs.Fire) {
            Shoot();
        }
        if (playerInputs.Reload) {
            Invoke("Reload", gunStats.gun.ReloadTime);
        }
        if (playerInputs.Dash && _canDash) {
            if (!TreeSkills.PlayerHasSkill(PlayerSkills.Dash)) return;
            _startDash = true;
        }
        if (_startDash) {
            _wallCollider.enabled = true;
            _trail.enabled = true;
            _trail.startColor = playerVisuals.GetHealthColor();
            _trail.endColor = playerVisuals.GetHealthColor();
            _invencible = true;
            Attack(_keyDirection);
        }
        if (!_isDashing) {
            _wallCollider.enabled = false;
            _invencible = false;
            _trail.enabled = false;
        }

        playerAnimation.SetBoolParam("IsMoving", isMoving);
    }

    private void FixedUpdate() {
        if (!_isDashing)
            Move(_keyDirection);
    }

    public void Shoot() {
        if (paused) return;
        if (gunStats.gun == null) return;
        if (!_canShoot) return;
        gunStats.gun.Fire(_hand, _firePoint, gameObject.layer);
        EventManager.instance.TriggerEvent("OnShoot");
        OnUpdateAmmo?.Invoke(gunStats.gun.Ammo);
        _canShoot = false;
        Invoke("CanShootAgain", gunStats.gun.FireRate);
    }

    public void CanShootAgain() {
        EventManager.instance.TriggerEvent("OnCanShoot");
        _canShoot = true;
    }

    public void Reload() {
        if (gunStats.gun.Ammo == gunStats.gun.MaxAmmo) return;
        gunStats.gun.Ammo = gunStats.gun.MaxAmmo;
        OnUpdateAmmo?.Invoke(gunStats.gun.Ammo);
        reloadTimer = 0;

    }



    public void UpdateReloadTimer(float time) {
        reloadTimerStart = time;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        IPickeupable pickeupable;
        if (!other.TryGetComponent<IPickeupable>(out pickeupable)) return;
        pickeupable.OnPickUp(this);
    }

    public void EnemyKill() {
        enemyDeadCounter++;
        if (enemyDeadCounter >= nextLevel) {
            LevelUp();
        }
    }

    private void LevelUp() {
        if (_currentLevel == _maxLevel) return;
        _currentLevel++;
        GameManager.instance._menuManagerUI.ShowTreeMenu();
        _uiTreeSkill.UpdateAbilitiesText();
        nextLevel = Mathf.CeilToInt(nextLevel * 2.5f);
        ScreenManager.instance.Pause();
    }

    public void RecoverHealth() {
        _health = _maxHealth;
    }

    protected override void Die() {
        base.Die();
        GameManager.instance.GameOver();
    }

    public void AsignGun() {
        gunStats.gun = GunContainer.GetGun(_gunsType);
        reloadTimerStart = gunStats.gun.ReloadTime;
        gunStats.Init();
    }

    public override void TakeDamage(int damage) {
        if (!_invencible) {
            base.TakeDamage(damage);
            playerVisuals.UpdateVisualHealth(_health, _maxHealth);
        }
    }

    public override void Pause() {
        base.Pause();
        isPaused = true;
        playerAnimation.StopAnimation();
    }

    public override void Resume() {
        base.Resume();
        playerAnimation.ResumeAnimation();
        isPaused = false;
    }

    private void OnSkillUnlocked(PlayerSkills skill) {
        switch (skill) {
            case PlayerSkills.Health_1:
                _maxHealth = 15;
                RecoverHealth();
                break;
            case PlayerSkills.Health_2:
                _maxHealth = 20;
                RecoverHealth();
                break;
            case PlayerSkills.Two_Shots:
                gunStats.gun.BulletsQty++;
                gunStats.gun.Spread = .35f;
                break;
            case PlayerSkills.Damage:
                gunStats.gun.Damage++;
                break;
            case PlayerSkills.FireRate:
                gunStats.gun.FireRate = .4f;
                break;
        }
    }

    protected virtual void Attack(Vector2 dir) {
        if (_timer <= 0) {
            _timer = _startTimeFloat;
            colldier.isTrigger = false;
            _rb.velocity = Vector2.zero;
            _isDashing = false;
            _startDash = false;
            StartCoroutine("DashAgain");
        }
        else {
            _timer -= Time.deltaTime;
            colldier.isTrigger = true;
            _isDashing = true;
            _canDash = false;
            _rb.velocity = dir.normalized * dashSpeed;
        }
    }
    IEnumerator DashAgain() {
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }

}
