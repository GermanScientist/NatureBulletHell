using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : Enemy {
    [SerializeField] private int shotgunPallets = 3;
    [SerializeField] private float palletAngle = 8;
    [SerializeField] private float palletCone = 35;
    
    protected override void Update() {
        base.Update();
        if (Vector2.Distance(transform.position, playerTransform.position) >= hoverDistance)
            ChasePlayer();
    }

    protected override void AttackPlayer() {
        if (Vector2.Distance(transform.position, playerTransform.position) < fireRange) {
            weapon.FireEnemyProjectile(playerDirection, projectileSpawn.position, 
                palletAngle, palletCone, shotgunPallets);
        }
    }
}
