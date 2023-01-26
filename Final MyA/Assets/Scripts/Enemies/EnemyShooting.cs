using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy {
    [SerializeField]
    private float shootDist;

    protected override void Start() {
        base.Start();
        GameManager.instance._eventManager.AddAction("CreateGuns", AsignGun);
        // AsignGun();
    }

    void Update() {
        UpdateHandPos();
        Move(Arrive(_target.position) + (Vector2)Separation());
        if (gun.Ammo < 1) {
            Reload();
        }
        RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _target.position - _firePoint.position, shootDist);
        Debug.DrawLine(_firePoint.position, _target.position, Color.black, 1);
        if (hitInfo.transform == null) return;
        if (hitInfo.transform.CompareTag("Player")) {
            if (dist <= shootDist) {
                _rb.velocity = Vector2.zero;
                Shoot();
            }
        }
    }



    private void AsignGun() {
        Debug.Log("Asignando arma enemigo");
        gun = GameManager.instance._gunPool.Get(_gunsType);
        if (gun != null) Debug.Log("Arma Asignada");
    }
    // private void UpdateHandPos() {
    //     var _dir = _target.position - _hand.position;
    //     float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
    //     _hand.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    // }

    protected override void OnDisable() {
        base.OnDisable();
        gun.notify.Invoke(gun.Name, gun);
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
