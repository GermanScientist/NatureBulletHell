using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimora : Enemy {
    [SerializeField] private float projectileLifespan = 1.5f;

    protected override void Start() {
        base.Start();
        firingSound = GameObject.Find("chimoraFire").GetComponent<AudioSource>();
    }

    protected override void Update() {
        base.Update();
        if (Vector2.Distance(transform.position, playerTransform.position) >= hoverDistance)
            ChasePlayer();
    }

    protected override void AttackPlayer() {
        if (Vector2.Distance(transform.position, playerTransform.position) < fireRange) {
            firingSound.Play();
            weapon.FireEnemyProjectile(playerDirection, projectileSpawn.position, projectileLifespan);
        }
    }
}
