using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBullets : InstanceClass<InstantiateBullets> {

    [SerializeField]
    public BulletPool bulletPool;

    [SerializeField]
    private Bullet[] _bulletsArr;


    protected void Start() {
        bulletPool.IntantiateBullets("Main Bullets", _bulletsArr[0], 25);
        bulletPool.IntantiateBullets("Enemy Bullets", _bulletsArr[1], 25);
        bulletPool.IntantiateBullets("Mini Bullets", _bulletsArr[2], 25);
        bulletPool.IntantiateBullets("Disperse Bullets", _bulletsArr[3], 25);
        bulletPool.IntantiateBullets("Intelligent Bullets", _bulletsArr[4], 25);
        //bulletPool.IntantiateBullets("Lightning Bullets", _bulletsArr[5], 3);
    }
}
