using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    [SerializeField]
    private Transform _target;
    private float _currentSpeed;
    private Vector2 _velocity;
    [SerializeField]
    private float viewRange;
    [SerializeField]
    protected Vector2 steering;

    private float dist;

    [SerializeField]
    private float shootDist;

    private void Start() {
        _target = PlayerManager.instance.transform;
        _currentSpeed = _speed;
        gun = GameManager.instance._gunPool.Get(_gunsType);
        Debug.Log(gun.Name);

    }


    void Update() {
        UpdateHandPos();
        Move(Arrive(_target.position));
        if (gun.Ammo < 1) {
            Reload();
        }
        RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _target.position - _firePoint.position, 20);
        Debug.DrawLine(_firePoint.position, _target.position, Color.black, 1);
        if (hitInfo.transform == null) return;
        if (hitInfo.transform.CompareTag("Player")) {
            if (dist < shootDist) {

                if (_canShoot)
                    _rb.velocity = Vector2.zero;
                Shoot();
            }
        }
    }

    private void UpdateHandPos() {
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

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);

    }

    private void OnDisable() {
        gun.notify.Invoke(gun.Name, gun);
    }
}
