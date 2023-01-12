using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBullets : InstanceClass<InstantiateBullets> {

    [SerializeField]
    public BulletPool bulletPool;

    [SerializeField]
    private Bullet _mainBullet;
    [SerializeField]
    private Bullet _enemyBullet;

    private void Start() {
        bulletPool.IntantiateBullets("Main Bullets", _mainBullet, 25);
        bulletPool.IntantiateBullets("Enemy Bullets", _enemyBullet, 25);
    }
}
