using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : Enemy {


    protected override void Update() {
        base.Update();
        Move(Arrive(_target.position) + (Vector2)Separation());
    }
}
