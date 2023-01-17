using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBullets : InstanceClass<InstantiateBullets>
{

    [SerializeField]
    public BulletPool bulletPool;

    [SerializeField]
    private Bullet _mainBullet;
    [SerializeField]
    private Bullet _enemyBullet;
    [SerializeField]
    private Bullet _miniBullet;
    [SerializeField]
    private Bullet _disperseBullet;

    protected override void Awake()
    {
        base.Awake();
        bulletPool.IntantiateBullets("Main Bullets", _mainBullet, 25);
        bulletPool.IntantiateBullets("Enemy Bullets", _enemyBullet, 25);
        bulletPool.IntantiateBullets("Mini Bullets", _miniBullet, 25);
        bulletPool.IntantiateBullets("Disperse Bullets", _disperseBullet, 25);
    }
}
