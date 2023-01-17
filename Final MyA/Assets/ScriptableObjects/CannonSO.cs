using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
[CreateAssetMenu(fileName = "Cannon", menuName = "ScriptableObjects/GunPart/Cannon")]
public class CannonSO : GunPartSO {
    [Tooltip("Increase value")]
    public float fireRate;
    [Tooltip("Increase value")]
    public float spread;
    [Tooltip("Replace value")]
    public int bulletsPerShot;
}
