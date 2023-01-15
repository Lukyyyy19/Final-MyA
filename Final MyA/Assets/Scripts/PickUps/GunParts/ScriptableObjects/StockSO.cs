using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunsEnum;
[CreateAssetMenu(fileName = "Stock", menuName = "ScriptableObjects/GunPart/Stock")]
public class StockSO : GunPartSO {
    [Tooltip("Increase value")]
    public float fireRate;
    [Tooltip("Increase value")]
    public float spread;

}
