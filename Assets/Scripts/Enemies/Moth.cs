using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : Enemy {
    protected override void Update() {
        base.Update();
        if (Vector2.Distance(transform.position, playerTransform.position) >= hoverDistance)
            ChasePlayer();
    }

    protected override void AttackPlayer() {
        if (Vector2.Distance(transform.position, playerTransform.position) < fireRange)
            weapon.FireEnemyProjectile(playerDirection, projectileSpawn.position);
    }
}
