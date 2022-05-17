using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : Projectile {
    public override void OnTriggerEnter2D(Collider2D _other) {
        if (_other.gameObject.tag == "Bullet" || _other.gameObject.tag == "Player") {
        }

        if (_other.gameObject.tag == "Enemy") {
            Enemy enemy = _other.gameObject.GetComponent<Enemy>();
            enemy.Damage(projectileStats.projectileDamage);

            Destroy(gameObject);
        }

        if (_other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
