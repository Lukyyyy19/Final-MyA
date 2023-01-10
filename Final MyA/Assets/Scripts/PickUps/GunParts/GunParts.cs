using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParts : MonoBehaviour, IPickeupable {

    public enum GunPart {
        Stock,
        Magazine,
        Barrel,
    }
    public GunPart gunPart;


    public void OnPickUp() {
        PlayerManager.instance.UpgradeGun(this);
        Debug.Log(gunPart);
        Destroy(gameObject);
    }


    public virtual void Attach(Gun gun) {

    }
    public virtual void Deattach(Gun gun) {

    }
}
