using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBullets : InstanceClass<InstantiateBullets> {

    [SerializeField]
    public BulletPool bulletPool;

    [SerializeField]
    private Bullet _bullet;

    private void Start() {
        bulletPool.IntantiateBullets("Main Bullets", _bullet, 25);
    }
}
