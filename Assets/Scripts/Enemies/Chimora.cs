using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimora : Enemy {
    protected override void Update() {
        base.Update();
        MoveTowards(target);
    }

    protected override void AttackPlayer() {
        if (Vector2.Distance(transform.position, playerTransform.position) < fireRange)
            weapon.FireEnemyProjectile(playerDirection, projectileSpawn.position);
    }
}
