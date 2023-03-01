using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisperseBullet : Bullet {

    BulletPool _bulletPool;

    Bullet[] _bullets = new Bullet[10];
    private void Start() {
        _bulletPool = InstantiateBullets.instance.bulletPool;
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        InstantiateMiniBullets();
        base.OnTriggerEnter2D(other);
    }

    private void InstantiateMiniBullets() {
        for (int i = 0; i < _bullets.Length; i++) {
            var angle = i * Mathf.PI * 2 / _bullets.Length;
            var pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 5;
            _bullets[i] = _bulletPool.Get("Mini Bullets", transform.position, pos);
            _bullets[i].gameObject.layer = gameObject.layer;
            _bullets[i].damage = 2;
            _bullets[i].currentSpeed = 4;
        }
    }
    protected override void TimeCompleted() {
        InstantiateMiniBullets();
        base.TimeCompleted();
    }
}
