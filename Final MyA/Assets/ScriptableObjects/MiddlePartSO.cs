using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
[CreateAssetMenu(fileName = "MiddlePart", menuName = "ScriptableObjects/GunPart/MiddlePart")]
public class MiddlePartSO : GunPartSO {

    [Tooltip("Replace value")]
    public int maxAmmo;
    [Tooltip("Increase value")]
    public float reloadTime;
    [Tooltip("Replace value")]
    public string bulletType;
    [Tooltip("Increase value")]
    public float damage;

}
