using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
using UpgradesEnum;
public class PlayerManager : EntityShoot {

    public delegate void UpdateAmmoDelegate(int bullet);
    public event UpdateAmmoDelegate OnUpdateAmmo;

    protected bool isPaused;
    private Vector2 _keyDirection;

    [Header("Components")]
    [SerializeField]
    private TrailRenderer _trail;
    [SerializeField]
    private ParticleSystem _dashParticle;
    [SerializeField]
    private BoxCollider2D _wallCollider;
    public GunStats _gunStats;
    public static PlayerManager instance;
    public PlayerInputs playerInputs;
    public PlayerAnimation playerAnimation;
    private TreeSkills _treeSkills;


    [HideInInspector]
    public float reloadTimer;
    [HideInInspector]
    public float reloadTimerStart;

    [Header("Level Count")]
    [SerializeField]
    private int enemyDeadCounter;
    [SerializeField]
    private int nextLevel = 3;

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




    public TreeSkills TreeSkills { get => _treeSkills; }

    protected override void Awake() {
        base.Awake();
        instance = this;
        playerInputs = new PlayerInputs();
        _gunStats = GetComponentInChildren<GunStats>();
        _timer = _startTimeFloat;
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
            _wallCollider.enabled = true;
            _trail.enabled = true;
            _trail.startColor = GetHealthColor();
            _trail.endColor = GetHealthColor();
            _invencible = true;
            Attack(_keyDirection);
        }
        if (!_isDashing) {
            _wallCollider.enabled = false;
            _invencible = false;
            _trail.enabled = false;
        }

        playerAnimation.SetBoolParam("IsMoving", isMoving);
        if (Input.GetKeyDown(KeyCode.F1)) _treeSkills.abilityPoints++;
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
        // StartCoroutine("DieStopTime");
        enemyDeadCounter++;
        if (enemyDeadCounter >= nextLevel) {
            LevelUp();
        }
    }

    private void LevelUp() {
        _treeSkills.abilityPoints++;
        GameManager.instance._menuManagerUI.ShowPauseMenu();
        nextLevel = Mathf.CeilToInt(nextLevel * 2.5f);
        ScreenManager.instance.Pause();
    }

    protected IEnumerator DieStopTime() {
        ScreenManager.instance.Pause();
        yield return new WaitForSeconds(.07f);
        ScreenManager.instance.Resume();
    }


    void AsignPlayerGun() {
        gun = GunContainer.GetGun(_gunsType);
        reloadTimerStart = gun.ReloadTime;
        _gunStats.Init();
    }
    protected override void OnDisable() {
        base.OnDisable();
        EventManager.instance.RemoveAction("CreateGuns", AsignPlayerGun);
    }
    public override void TakeDamage(int damage) {
        if (!_invencible) {
            base.TakeDamage(damage);
            UpdateVisualHealth();

        }
    }

    private void UpdateVisualHealth() {
        float healthVisual = (float)_health / (float)_maxHealth;
        sr.material.SetFloat("_Health", healthVisual);
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

    Color GetHealthColor() {
        var damagedColor = sr.material.GetColor("_damaged");
        var fullHealthColor = sr.material.GetColor("_fullHealth");
        var t = sr.material.GetFloat("_Health");
        return Color.Lerp(damagedColor, fullHealthColor, t);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected virtual void Attack(Vector2 dir) {
        if (_timer <= 0) {
            _timer = _startTimeFloat;
            colldier.isTrigger = false;
            _rb.velocity = Vector2.zero;
            _isDashing = false;
            _startDash = false;
            StartCoroutine("DashAgain");
        } else {
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
