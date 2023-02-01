using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public abstract class Entity : MonoBehaviour, IDamageable, IPausable {

    protected float _speed;
    [SerializeField]
    protected float _maxSpeed;
    protected Rigidbody2D _rb;

    [SerializeField]
    public Gun gun;

    [SerializeField]
    protected GunsType _gunsType;

    [SerializeField]
    protected bool _canShoot;

    [SerializeField]
    protected int _health;

    [SerializeField]
    protected int _maxHealth;

    [SerializeField]
    protected Transform _firePoint;

    [SerializeField]
    protected Transform _hand;

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

    protected bool paused;

    protected Collider2D colldier;

    protected SpriteRenderer sr;
    private Color _mainColor;

    protected bool isMoving;


    public delegate void ShootDelegate();
    public event ShootDelegate OnShoot;

    public delegate void CanShootDelegate();
    public event ShootDelegate OnCanShoot;

    [SerializeField]
    protected ParticleSystem particleDead;
    protected virtual void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _health = _maxHealth;
        _speed = _maxSpeed;
        _timer = _startTimeFloat;
        colldier = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        _mainColor = sr.color;
    }
    protected virtual void Start() {

        ScreenManager.instance.AddPausable(this);
    }

    protected virtual void Shoot() {
        if (gun == null) return;
        if (!_canShoot) return;
        if (gun.Ammo < 1) return;
        if (paused) return;
        OnShoot?.Invoke();
        _canShoot = false;
        for (int i = 0; i < gun.BulletsQty; i++) {

            Vector2 dir = _hand.rotation * Vector2.down;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-gun.Spread, gun.Spread);
            var bullet = InstantiateBullets.instance.bulletPool.Get(gun.BulletType, _firePoint.position, (dir + pdir));
            //Cambiar de Layer para que nuestra bala no nos golpee
            bullet.damage = gun.Damage;

        }

        gun.Ammo--;
        Invoke("CanShootAgain", gun.FireRate);
    }

    void CanShootAgain() {
        OnCanShoot?.Invoke();
        _canShoot = true;
    }

    protected virtual void Reload() {
        if (gun.Ammo == gun.MaxAmmo) return;
        gun.Ammo = gun.MaxAmmo;

    }
    public virtual void TakeDamage(int damage) {
        _health -= damage;
        sr.color = Color.white;
        Invoke("ResetColor", 0.05f);
        if (_health <= 0) {
            Die();
        }
    }
    protected virtual void Die() {
        //HACER POOL
        sr.enabled = false;
        _rb.isKinematic = true;
        ResetColor();
        var currParticle = Instantiate(particleDead, transform.position, Quaternion.identity);
        var pm = currParticle.main;
        pm.startColor = sr.color;
        gameObject.SetActive(false);
    }


    void ResetColor() {
        sr.color = _mainColor;
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

    protected void Move(Vector2 dir) {
        _rb.velocity = dir.normalized * _speed;
        isMoving = _rb.velocity.magnitude > 0;

    }


    protected virtual void OnDisable() {
        ScreenManager.instance.RemovePausable(this);
    }

    public virtual void Pause() {
        _speed = 0;
        _rb.isKinematic = true;
        paused = true;

    }

    public virtual void Resume() {
        _speed = _maxSpeed;
        _rb.isKinematic = false;
        paused = false;

    }
}
