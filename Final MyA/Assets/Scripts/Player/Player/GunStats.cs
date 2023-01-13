using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
public class GunStats : MonoBehaviour {

    public HashSet<GunPart> upgrades = new HashSet<GunPart>();
    [SerializeField]
    public Gun gun;
    GunsType currentGun;
    // void Start() {
    //     gun = GunContainer.GetGun(currentGun);
    //     upgrades.Add(GetComponent<Magazine>().gunPart);
    //     upgrades.Add(GetComponent<Stock>().gunPart);
    //     upgrades.Add(GetComponent<Barrel>().gunPart);
    //     PlayerManager.instance.gun = gun;

    // }
    // private void Update() {
    //     Debug.Log(upgrades.Count);
    // }

    // public void UpgradeGun(GunParts gunPart) {
    //     if (upgrades.Add(gunPart.gunPart)) {
    //         gunPart.Attach(gun);
    //     } else {
    //         DowngradeGun(gunPart);
    //         UpgradeGun(gunPart);
    //     }
    //     gun.ClampValues();
    //     // OnUpdateAmmo?.Invoke(gun.Ammo);
    // }
    // public void DowngradeGun(GunParts gunPart) {
    //     if (upgrades.Remove(gunPart.gunPart)) {
    //         Debug.Log("Removiendo");
    //         gunPart.Deattach(gun);
    //     }
    //     gun.ClampValues();
    //     //OnUpdateAmmo?.Invoke(gun.Ammo);

    // }
}
