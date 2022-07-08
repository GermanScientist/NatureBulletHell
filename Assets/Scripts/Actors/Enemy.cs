using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Actor {
    [SerializeField] protected float fireRate = 1f;

    protected Vector3 playerDirection;
    protected Transform playerTransform;

    protected override void Start() {
        base.Start();
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("AttackPlayer", 0, fireRate);
    }

    protected virtual void Update() {
        AimAtPlayer();
    }

    public virtual void Move(Vector2 _target) {
        transform.position = Vector2.MoveTowards(transform.position, _target, movementSpeed * Time.deltaTime);
    }

    protected void AimAtPlayer() {
        playerDirection = (playerTransform.position - this.transform.position).normalized;
    }

    protected virtual void ChasePlayer() {
        Move(movementSpeed, playerDirection);
    }

    protected virtual void AttackPlayer() {

    }
}
