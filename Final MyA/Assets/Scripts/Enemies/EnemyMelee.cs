using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy {
    [SerializeField]
    private float _dashMinDist;

    // protected override void Start() {
    //     base.Start();
    //     radiusSeparation = 5.3f;
    //     GameManager.instance.enemies.Add(this);
    //     _target = PlayerManager.instance.transform;
    //     _currentSpeed = _speed;
    // }
    void Update() {
        UpdateHandPos();
        Move(Arrive(_target.position) + (Vector2)Separation());
        RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _target.position - _firePoint.position, viewRange);
        Debug.DrawLine(_firePoint.position, _target.position, Color.magenta, 1);
        if (hitInfo.transform == null) return;
        if (hitInfo.transform.CompareTag("Player")) {
            if (dist <= _dashMinDist && _canDash) {
                // Attack(steering);
                _rb.velocity = Vector2.zero;
            }
        }
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusSeparation);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _dashMinDist);

    }

}
