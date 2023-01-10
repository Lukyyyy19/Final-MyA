using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletPool : MonoBehaviour {
    Dictionary<string, PoolObject<Bullet>> pools = new Dictionary<string, PoolObject<Bullet>>();


    public void IntantiateBullets(string _key, Bullet bulletType, int prewarm) {
        var bulletPoolTemp = new GameObject();
        bulletPoolTemp.name = "Pool de " + _key;
        Func<Bullet> bulletFunc = () => {
            Bullet bullet = GameObject.Instantiate(bulletType, Vector2.zero, Quaternion.identity);
            bullet.transform.parent = bulletPoolTemp.transform;
            bullet.Configure(_key, Return);
            return bullet;
        };

        if (pools != null) {
            if (!pools.ContainsKey(_key)) {
                PoolObject<Bullet> currentPool = new PoolObject<Bullet>();
                currentPool.Intialize(TurnOnBullet, TurnOffBullet, bulletFunc, prewarm);
                pools.Add(_key, currentPool);
            }
        }
    }

    public Bullet Get(string _key, Vector2 pos, Vector2 dir) {
        Bullet myBullet = pools[_key].Get();
        myBullet.Move(pos, dir);
        return myBullet;
    }


    public void Return(string _key, Bullet obj) {
        pools[_key].Return(obj);
    }


    void TurnOnBullet(Bullet bullet) {
        bullet.gameObject.SetActive(true);
        bullet.Prender();
    }

    void TurnOffBullet(Bullet bullet) {
        bullet.Apagar();
        bullet.gameObject.SetActive(false);
    }
}
