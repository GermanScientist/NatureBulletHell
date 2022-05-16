using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private WeaponSO projectileStats;
    private int currentAmmo;

    public int CurrentAmmo { get { return currentAmmo; } }

    private void Start() {
        currentAmmo = projectileStats.maxAmmo;
    }

    public void FireProjectile(Vector3 _direction) {
        //The spawn position will be dependant of the direction the player is aiming
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, -1) + _direction * projectileStats.projectileSpawnDistance;

        //Instantiate a bullet using the information from the player's projectile scriptableobject
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(_direction));
        Projectile p = projectile.AddComponent<Projectile>();
        p.ProjectileStats = projectileStats;

        currentAmmo--; //Update the ammo acount
    }
}
