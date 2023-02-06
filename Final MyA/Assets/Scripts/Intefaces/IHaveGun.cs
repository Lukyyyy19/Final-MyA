using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveGun {
    void Shoot();
    void CanShootAgain();
    void Reload();
}
