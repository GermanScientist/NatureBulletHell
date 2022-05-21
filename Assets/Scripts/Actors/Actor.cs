using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Weapon))]
public abstract class Actor : MonoBehaviour {
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float collisionOffset = 0.05f;
    [SerializeField] protected int maxHitpoints;
    
    protected int currentHitpoints;

    protected Rigidbody2D rb;
    protected Weapon weapon;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    protected Vector2 moveDirection;
    protected Transform projectileSpawn;

    public int Hitpoints { get { return currentHitpoints; } }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        weapon = GetComponent<Weapon>();

        currentHitpoints = maxHitpoints;
        projectileSpawn = transform.GetChild(0).transform;
    }

    protected void Move(float _speed, Vector2 _direction) {
        Vector2 speed = new Vector2(_direction.x * _speed, _direction.y * _speed);
        speed.Normalize();

        bool succes = CheckToMove(_direction, _speed); //Try to move, move if there is no collision
        if (!succes) { //If there is no collision, allowing the player to move...
            succes = CheckToMove(new Vector2(_direction.x, 0), _speed); //Try to move left/right
            if (!succes) { //If we can't move left/right, try to move up/down
                succes = CheckToMove(new Vector2(0, _direction.y), _speed);
            }
        }
    }

    private bool CheckToMove(Vector2 _direction, float _speed) {
        //Check for potential collisions
        int count = rb.Cast(_direction, movementFilter, castCollisions, _speed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0) { //No collision detected
            Vector2 moveVector = _direction.normalized * _speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        return false; //If a collision has been detected, return false
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }

    public virtual void Damage(int _damageAmount) {
        currentHitpoints -= _damageAmount;
        if (currentHitpoints <= 0) Die();
    }
}
