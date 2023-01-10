using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class Entity : MonoBehaviour, IDamageable {

    [SerializeField]
    protected float _speed;
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
    protected ArmMovement _hand;

    public delegate void ShootDelegate();
    public event ShootDelegate OnShoot;

    protected virtual void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Shoot() {
        if (gun == null) return;
        if (!_canShoot) return;
        if (gun.Ammo < 1) return;
        OnShoot?.Invoke();
        _canShoot = false;
        for (int i = 0; i < gun.BulletsQty; i++) {

            Vector2 dir = _hand.transform.rotation * Vector2.down;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-gun.Spread, gun.Spread);
            var bullet = InstantiateBullets.instance.bulletPool.Get(gun.BulletType, _firePoint.position, (dir + pdir));
            //Cambiar de Layer para que nuestra bala no nos golpee
            bullet.damage = gun.Damage;

        }

        gun.Ammo--;
        Invoke("CanShootAgain", gun.FireRate);
    }

    void CanShootAgain() {
        _canShoot = true;
    }

    protected virtual void Reload() {
        if (gun.Ammo == gun.MaxAmmo) return;
        gun.Ammo = gun.MaxAmmo;

    }

    protected virtual void Die() {
        gameObject.SetActive(false);
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


}
