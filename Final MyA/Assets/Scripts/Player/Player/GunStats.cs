using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class GunStats : MonoBehaviour {

    public HashSet<GunParts> upgrades = new HashSet<GunParts>();
    public Gun gun;
    GunsType currentGun;
    void Start() {
        upgrades.Add(GetComponent<Magazine>());
        upgrades.Add(GetComponent<Stock>());
        upgrades.Add(GetComponent<Barrel>());


    }


    void Update() {

    }

    public void UpgradeGun(GunParts gunPart) {
        if (upgrades.Add(gunPart)) {
            gunPart.Attach(gun);
        } else {
            DowngradeGun(gunPart);
            UpgradeGun(gunPart);
        }
        gun.ClampValues();
        // OnUpdateAmmo?.Invoke(gun.Ammo);
    }
    public void DowngradeGun(GunParts gunPart) {
        if (upgrades.Remove(gunPart)) {
            gunPart.Deattach(gun);
        }
        gun.ClampValues();
        //OnUpdateAmmo?.Invoke(gun.Ammo);

    }
}
