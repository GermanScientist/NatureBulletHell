using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : Projectile {
    public override void OnCollisionEnter2D(Collision2D _other) {
        if (_other.gameObject.tag == "Bullet" || _other.gameObject.tag == "Player") {
            IgnoreCollision(_other);
        }

        if (_other.gameObject.tag == "Enemy") {
            IgnoreCollision(_other);

            Enemy enemy = _other.gameObject.GetComponent<Enemy>();
            enemy.Damage(projectileStats.projectileDamage);

            Destroy(gameObject);
        }

        if (_other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
