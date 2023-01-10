using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class GunContainer : MonoBehaviour {
    //FireRate, BulletType, MaxAmmo, ReloadTime, Damage, Spread, GunName, BulletsPerShot
    public Gun pistol = new Gun(.75f, "Main Bullets", 12, .5f, 1, .1f, GunsType.Pistol, 1);
    public Gun rifle = new Gun(.2f, "Main Bullets", 30, 1f, 1, .05f, GunsType.Rifle, 1);
    public Gun shotgun = new Gun(1, "Main Bullets", 5, .5f, 1, .3f, GunsType.Shotgun, 3);


    public static Dictionary<GunsType, Gun> guns;
    private void Awake() {
        guns = new Dictionary<GunsType, Gun>();
        guns.Add(pistol.Name, pistol);
        guns.Add(rifle.Name, rifle);
        guns.Add(shotgun.Name, shotgun);

    }

    public static Gun GetGun(GunsType gunName) {
        return guns[gunName];
    }
}
