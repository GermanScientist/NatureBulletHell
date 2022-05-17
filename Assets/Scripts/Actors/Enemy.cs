using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Actor {
    [SerializeField] protected float fireRate = 1f;

    protected Vector3 aimDirection;
    protected Transform playerTransform;

    protected override void Start() {
        base.Start();
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("AttackPlayer", 0, fireRate);
    }

    private void Update() {
        AimAtPlayer();
    }

    protected void AimAtPlayer() {
        aimDirection = (playerTransform.position - this.transform.position).normalized;
    }

    protected virtual void ChasePlayer() {

    }

    protected virtual void AttackPlayer() {

    }
}
