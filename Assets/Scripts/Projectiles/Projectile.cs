using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {
    private Rigidbody2D rb;
    private WeaponSO projectileStats;
    
    public WeaponSO ProjectileStats { set { projectileStats = value; } }

    private void Start() {
        //Remove the bullet's gravity and shoot the bullet towards the direction of the mouse when the bullet was fired
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = transform.forward * projectileStats.projectileSpeed;

        gameObject.AddComponent<CircleCollider2D>();
        gameObject.tag = "Bullet";
    }

    public void OnCollisionEnter2D(Collision2D _other) {
        if (_other.gameObject.tag == "Bullet" || _other.gameObject.tag == "Player") {
            Collider2D otherCollider = _other.gameObject.GetComponent<Collider2D>();
            Collider2D collider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(otherCollider, collider);
        }

        if (_other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
