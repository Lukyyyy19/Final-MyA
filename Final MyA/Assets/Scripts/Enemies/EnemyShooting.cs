using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
using System;

public class EnemyShooting : Enemy, IHaveGun {
    [SerializeField]
    private float shootDist;
    [SerializeField]
    private float stopDist;
    [SerializeField]
    public Gun gun;
    [SerializeField]
    protected GunsType _gunsType;
    protected bool _canShoot = true;
    [SerializeField]
    protected Transform _firePoint;

    protected override void Start() {
        base.Start();
        AsignGun();
    }

    protected override void Update() {
        base.Update();
        if (_isSpawning) return;
        Move(Arrive(_target.position) + (Vector2)Separation());
        if (gun.Ammo < 1) {
            Reload();
        }
        RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _target.position - _firePoint.position, shootDist);
        Debug.DrawLine(_firePoint.position, _target.position, Color.black, 1);
        if (hitInfo.transform == null) return;
        if (hitInfo.transform.CompareTag("Player")) {
            if (dist <= shootDist) {
                Shoot();
                if (dist <= stopDist)
                    _rb.velocity = Vector2.zero;
            }
        }
    }

    public void AsignGun() {
        gun = GameManager.instance._gunPool.Get(_gunsType);
    }

    protected override void OnDisable() {
        base.OnDisable();
        if (gun != null)
            gun.notify.Invoke(gun.Name, gun);
    }

    public void Shoot() {
        if (paused) return;
        if (gun == null) return;
        if (!_canShoot) return;
        gun.Fire(_hand, _firePoint, gameObject.layer);
        _canShoot = false;
        Invoke("CanShootAgain", gun.FireRate);
    }

    public void CanShootAgain() {
        //EventManager.instance.TriggerEvent("OnCanShoot");
        _canShoot = true;
    }

    public void Reload() {
        if (gun.Ammo == gun.MaxAmmo) return;
        gun.Ammo = gun.MaxAmmo;

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusSeparation);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, shootDist);

    }
}
