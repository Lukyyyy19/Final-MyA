using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class Entity : MonoBehaviour, IDamageable, IPausable {

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
    private float _startTimeFloat = .1f;
    [SerializeField]
    private float _timer;
    private bool _startTime;




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
    }
    protected virtual void Start() {

        ScreenManager.instance.AddPausable(this);
    }

    protected virtual void Shoot() {
        if (gun == null) return;
        if (!_canShoot) return;
        if (gun.Ammo < 1) return;
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

    protected virtual void Die() {
        //HACER POOL
        var currParticle = Instantiate(particleDead, transform.position, Quaternion.identity);
        gameObject.SetActive(false);

    }

    protected void Attack(Vector2 dir) {
        if (_timer <= 0) {
            _timer = _startTimeFloat;
            _rb.velocity = Vector2.zero;
            _isDashing = false;
            _startDash = false;
            StartCoroutine("DashAgain");
        } else {
            _timer -= Time.deltaTime;
            _isDashing = true;
            _canDash = false;
            _rb.velocity = dir * dashSpeed;
        }
    }
    IEnumerator DashAgain() {
        yield return new WaitForSeconds(1f);
        _canDash = true;
    }

    protected void Move(Vector2 dir) {
        _rb.velocity = dir.normalized * _speed;
    }

    public virtual void TakeDamage(int damage) {
        _health -= damage;
        if (_health <= 0) {
            Die();
        }
    }
    protected virtual void OnDisable() {
        ScreenManager.instance.RemovePausable(this);
    }

    public virtual void Pause() {
        _speed = 0;
        _rb.isKinematic = true;
    }

    public virtual void Resume() {
        _speed = _maxSpeed;
        _rb.isKinematic = false;
    }
}
