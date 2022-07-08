using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Actor {
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float chaseDistance = 40f;
    [SerializeField] protected float fireRange = 8;
    [SerializeField] protected float hoverDistance = 8.5f;

    protected Vector3 playerDirection;
    protected Vector2 target;
    protected Transform playerTransform;
    
    private bool canMove;

    public Vector2 Target { set { target = value; } }
    public float ChaseDistance { get { return chaseDistance; } }
    public bool CanMove { set { canMove = value; } }

    protected override void Start() {
        base.Start();
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("AttackPlayer", 0, fireRate);
    }

    protected virtual void Update() {
        AimAtPlayer();
    }

    public virtual void MoveTowards(Vector2 _target) {
        if (_target != null && canMove) {
            transform.position = Vector2.MoveTowards(transform.position, _target,
                movementSpeed * Time.deltaTime);
        }
    }

    protected virtual void ChasePlayer() {
        MoveTowards(target);
        Vector2 dir = (transform.position - playerTransform.transform.position).normalized;
        if (dir.x > 0.5f && spriteRenderer != null) spriteRenderer.flipX = false;
        if (dir.x < -0.5f && spriteRenderer != null) spriteRenderer.flipX = true;
    }

    protected void AimAtPlayer() {
        playerDirection = (playerTransform.position - this.transform.position).normalized;
    }

    protected virtual void AttackPlayer() {

    }
}
