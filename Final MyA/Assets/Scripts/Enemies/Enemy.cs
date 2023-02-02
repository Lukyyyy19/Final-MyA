using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    [SerializeField]
    protected Transform _target;
    protected float _currentSpeed;
    protected Vector2 _velocity;
    [SerializeField]
    protected float viewRange;

    protected float radiusSeparation = 5.3f;
    [SerializeField]
    protected Vector2 steering;

    protected float dist;

    [SerializeField]
    protected ParticleSystem particleDead;

    protected override void Start() {
        base.Start();
        radiusSeparation = 5.3f;
        GameManager.instance.enemies.Add(this);
        _target = PlayerManager.instance.transform;
        _currentSpeed = _speed;
    }

    // private void AsignGun() {
    //     Debug.Log("Asignando arma enemigo");
    //     gun = GameManager.instance._gunPool.Get(_gunsType);
    // }

    // void Update() {
    //     UpdateHandPos();
    //     Move(Arrive(_target.position) + (Vector2)Separation());
    //     if (gun.Ammo < 1) {
    //         Reload();
    //     }
    //     RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _target.position - _firePoint.position, shootDist);
    //     Debug.DrawLine(_firePoint.position, _target.position, Color.black, 1);
    //     if (hitInfo.transform == null) return;
    //     if (hitInfo.transform.CompareTag("Player")) {
    //         if (dist <= shootDist) {
    //             _rb.velocity = Vector2.zero;
    //             Shoot();
    //         }
    //     }
    // }

    protected void UpdateHandPos() {
        var _dir = _target.position - _hand.position;
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        _hand.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }

    protected Vector2 Arrive(Vector3 actualTarget) {
        Vector2 desired = actualTarget - transform.position;
        dist = desired.magnitude;
        desired.Normalize();
        if (dist <= viewRange) {
            desired *= _currentSpeed * (dist / viewRange);
        } else {
            desired *= _currentSpeed;
        }

        steering = desired;

        return steering;
    }

    protected Vector3 Separation() {
        Vector3 desired = Vector3.zero;

        foreach (var item in GameManager.instance.enemies) {
            Vector3 dist = item.transform.position - transform.position;

            if (dist.magnitude <= radiusSeparation)
                desired += dist;
        }

        if (desired == Vector3.zero)
            return desired;

        desired = -desired;

        desired.Normalize();
        desired *= _speed;

        return desired;
    }

    protected override void Die() {
        base.Die();
        sr.enabled = false;
        _rb.isKinematic = true;
        ResetColor();
        var currParticle = Instantiate(particleDead, transform.position, Quaternion.identity);
        var pm = currParticle.main;
        pm.startColor = sr.color;
        PlayerManager.instance.EnemyKill();
    }



}
