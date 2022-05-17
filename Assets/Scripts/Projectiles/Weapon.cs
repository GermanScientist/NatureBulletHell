using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private WeaponSO projectileStats;
    private int currentAmmo;

    public int CurrentAmmo { get { return currentAmmo; } }

    private void Start() {
        if (projectileStats != null)
            currentAmmo = projectileStats.maxAmmo;
    }

    public void FireFriendlyProjectile(Vector3 _direction) {
        //The spawn position will be dependant of the direction the player is aiming
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, -1) + _direction * projectileStats.projectileSpawnDistance;

        //Instantiate a bullet using the information from the player's projectile scriptableobject
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(_direction));
        
        //Calculate the rotation of the bullet
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        
        //Add the projectile component
        FriendlyProjectile p = projectile.AddComponent<FriendlyProjectile>();
        p.ProjectileStats = projectileStats;

        currentAmmo--; //Update the ammo acount
    }

    public void FireEnemyProjectile(Vector3 _direction) {
        //The spawn position will be dependant of the direction the player is aiming
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, -1) + _direction * projectileStats.projectileSpawnDistance;

        //Instantiate a bullet using the information from the player's projectile scriptableobject
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(_direction));
        
        //Calculate the rotation of the bullet
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        //Add the projectile component
        EnemyProjectile p = projectile.AddComponent<EnemyProjectile>();
        p.ProjectileStats = projectileStats;

        currentAmmo--; //Update the ammo acount
    }
}
