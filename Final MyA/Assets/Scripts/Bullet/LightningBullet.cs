using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : Bullet {
    void ThrowLightning() {
        Physics2D.CircleCast(transform.position, 2, direction);
        Debug.DrawRay(transform.position, direction, Color.green, .1f);

    }
}
