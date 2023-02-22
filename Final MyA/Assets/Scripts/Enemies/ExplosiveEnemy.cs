using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : Enemy {
    [SerializeField]
    private float _explosionDsit;
    [SerializeField]
    LayerMask _playerLayer;
    [SerializeField]
    private int _bulletsToShoot;


    protected override void Update() {
        base.Update();
        if (_isSpawning) return;
        Move(Arrive(_target.position) + (Vector2)Separation());
        var playerColl = Physics2D.OverlapCircle(transform.position, _explosionDsit);
        if (playerColl.CompareTag("Player")) {
            Die();
        }
    }

    protected override void Die() {
        Explode();
        base.Die();
    }




    private void Explode() {
        var _bulletPool = InstantiateBullets.instance.bulletPool;
        Bullet[] _bullets = new Bullet[_bulletsToShoot];
        _rb.velocity = Vector2.zero;
        for (int i = 0; i < _bulletsToShoot; i++) {
            var angle = i * Mathf.PI * 2 / _bulletsToShoot;
            var pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 5;
            _bullets[i] = _bulletPool.Get("Enemy Bullets", transform.position, pos);
            _bullets[i].damage = 1;
            _bullets[i].gameObject.layer = gameObject.layer;
            _bullets[i].currentSpeed = 4;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusSeparation);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _explosionDsit);
    }
}
