using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy {
    [SerializeField]
    private float _dashMinDist;

    private bool _canAttack = true;
    [SerializeField]
    private int damage;

    protected override void Update() {
        base.Update();
        if (_isSpawning) return;
        Move(Arrive(_target.position) + (Vector2)Separation());
    }

    private void OnCollisionStay2D(Collision2D other) {
        IDamageable damageable;
        if (other.gameObject.TryGetComponent<IDamageable>(out damageable)) {
            if (_canAttack) {
                damageable.TakeDamage(damage);
                _canAttack = false;
                Invoke("CanAttackAgain", 1.5f);
            }
        }
    }

    private void CanAttackAgain() {
        _canAttack = true;
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
