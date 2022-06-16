using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : Enemy {
    protected override void Update() {
        base.Update();
        //ChasePlayer();
    }

    protected override void AttackPlayer() {
        if (Vector2.Distance(transform.position, playerTransform.position) < 10)
            weapon.FireEnemyProjectile(playerDirection, projectileSpawn.position);
    }
}
