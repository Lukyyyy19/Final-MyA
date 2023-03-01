using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class GunContainer : InstanceClass<GunContainer> {
    //FireRate, BulletType, MaxAmmo, ReloadTime, Damage, Spread, GunName, BulletsPerShot
    public Gun playerGun = new Gun(.75f, "Intelligent Bullets", 15, .4f, 1, .2f, GunsType.PlayerGun, 1);
    public Gun pistol = new Gun(.75f, "Main Bullets", 12, .5f, 1, .1f, GunsType.Pistol, 1);
    public Gun rifle = new Gun(.2f, "Main Bullets", 30, 1f, 1, .05f, GunsType.Rifle, 1);
    public Gun shotgun = new Gun(1, "Main Bullets", 5, .5f, 1, .3f, GunsType.Shotgun, 3);

    bool initialized;
    public static Dictionary<GunsType, Gun> guns;

    protected override void Awake() {
        base.Awake();
        guns = new Dictionary<GunsType, Gun>();
        guns.Add(playerGun.Name, playerGun);
        guns.Add(pistol.Name, pistol);
        guns.Add(rifle.Name, rifle);
        guns.Add(shotgun.Name, shotgun);
    }

    private void Start() {
        Init();
    }
    public void Init() {
        if (initialized) return;

        EventManager.instance.TriggerEvent("CreateGuns");
        initialized = true;

    }

    public static Gun GetGun(GunsType gunName) {
        return guns[gunName];
    }
}
