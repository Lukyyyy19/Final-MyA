using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;

public class GunParts : MonoBehaviour, IPickeupable {
    private Gun gunInfo;

    public virtual void OnPickUp() {

    }

    // private void OnEnable() {
    //     GunContainer.OnCreate += InitialStats;
    // }
    // private void OnDisable() {
    //     GunContainer.OnCreate -= InitialStats;
    // }
    // protected virtual void InitialStats() { Debug.Log("asignando valores"); }
    // public virtual void Attach(Gun gun) {

    // }
    // public virtual void Deattach(Gun gun) {

    // }
}
