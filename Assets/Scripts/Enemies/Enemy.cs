using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    protected int currentHitpoints;

    [SerializeField] protected int maxHitpoints;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float fireRate = 1f;

    protected Vector3 aimDirection;
    protected Transform playerTransform;
    protected Weapon weapon;

    public int Hitpoints { get { return currentHitpoints; } }

    private void Start() {
        GetComponent<Rigidbody2D>().isKinematic = true;
        weapon = GetComponent<Weapon>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentHitpoints = maxHitpoints;

        InvokeRepeating("AttackPlayer", 0, fireRate);
    }

    private void Update() {
        AimAtPlayer();
    }

    public void Damage(int _damageAmount) {
        currentHitpoints -= _damageAmount;
        Debug.Log(currentHitpoints);
        if (currentHitpoints <= 0) Die();
    }

    protected void AimAtPlayer() {
        aimDirection = (playerTransform.position - this.transform.position).normalized;
    }

    protected virtual void ChasePlayer() {

    }

    protected virtual void AttackPlayer() {

    }

    private void Die() {
        Destroy(gameObject);
    }
}
