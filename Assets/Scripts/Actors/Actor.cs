using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Weapon))]
public abstract class Actor : MonoBehaviour {
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected int maxHitpoints;

    protected int currentHitpoints;
    
    protected Rigidbody2D rb;
    protected Weapon weapon;

    public int Hitpoints { get { return currentHitpoints; } }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        weapon = GetComponent<Weapon>();

        currentHitpoints = maxHitpoints;
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }

    public virtual void Damage(int _damageAmount) {
        currentHitpoints -= _damageAmount;
        if (currentHitpoints <= 0) Die();
    }
}
