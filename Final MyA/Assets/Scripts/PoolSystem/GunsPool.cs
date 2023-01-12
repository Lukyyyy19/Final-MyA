using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
using System;


public class GunsPool : MonoBehaviour {
    Dictionary<GunsType, PoolObject<Gun>> pools = new Dictionary<GunsType, PoolObject<Gun>>();


    public void IntantiateGuns(GunsType gunType, int prewarm) {
        var GunPoolTemp = new GameObject();
        GunPoolTemp.name = "Pool de " + gunType.ToString();
        Func<Gun> GunFunc = () => {
            Gun gun = (Gun)GunContainer.GetGun(gunType).Clone();
            gun.Configure(Return);
            gun.BulletType = "Enemy Bullets";
            return gun;
        };

        if (pools != null) {
            if (!pools.ContainsKey(gunType)) {
                PoolObject<Gun> currentPool = new PoolObject<Gun>();
                currentPool.Intialize(GunFunc, prewarm);
                pools.Add(gunType, currentPool);
            }
        }
    }

    public Gun Get(GunsType _key) {
        Gun myGun = pools[_key].Get();
        return myGun;
    }


    public void Return(GunsType _key, Gun obj) {
        pools[_key].Return(obj);
    }


    // void TurnOnGun(Gun Gun) {
    //     Gun.gameObject.SetActive(true);
    //     Gun.Prender();
    // }

    // void TurnOffGun(Gun Gun) {
    //     Gun.Apagar();
    //     Gun.gameObject.SetActive(false);
    // }
}
