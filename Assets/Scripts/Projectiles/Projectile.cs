using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour {
    private Rigidbody2D rb;
    protected WeaponSO projectileStats;
    
    public WeaponSO ProjectileStats { set { projectileStats = value; } }

    private void Start() {
        //Remove the bullet's gravity and shoot the bullet towards the direction of the mouse when the bullet was fired
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
        gameObject.tag = "Bullet";

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void FixedUpdate() {
        rb.MovePosition(transform.position + transform.up*projectileStats.projectileSpeed);
    }

    public virtual void OnTriggerEnter2D(Collider2D _other) {
        if (_other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
