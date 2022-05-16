using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    protected int currentHitpoints;

    [SerializeField] protected int maxHitpoints;
    [SerializeField] protected float movementSpeed;

    protected Vector3 aimDirection;

    protected Player player;

    public int Hitpoints { get { return currentHitpoints; } }

    private void Start() {
        
    }

    public void Damage(int _damageAmount) {
        currentHitpoints -= _damageAmount;
    }

    protected void AimAtPlayer() {

    }

    protected virtual void ChasePlayer() {

    }

    protected virtual void AttackPlayer() {

    }
}
