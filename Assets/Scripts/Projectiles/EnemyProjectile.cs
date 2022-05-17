using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {
    public override void OnCollisionEnter2D(Collision2D _other) {
        if (_other.gameObject.tag == "Bullet" || _other.gameObject.tag == "Enemy") {
            IgnoreCollision(_other);
        }

        if (_other.gameObject.tag == "Player") {
            Player player = _other.gameObject.GetComponent<Player>();
            player.Damage(projectileStats.projectileDamage);

            Destroy(gameObject);
        }

        if (_other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
