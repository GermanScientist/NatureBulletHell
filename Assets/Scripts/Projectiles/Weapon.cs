using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private WeaponSO projectileStats;
    private int currentAmmo;

    public int CurrentAmmo { get { return currentAmmo; } }

    private void Awake() {
        if (projectileStats != null)
            currentAmmo = projectileStats.maxAmmo;
    }

    public void FireFriendlyProjectile(Vector3 _direction, Vector3 _spawnPosition) {
        GameObject projectile = CreateProjectile(_direction, _spawnPosition);

        //Add the projectile component
        FriendlyProjectile p = projectile.AddComponent<FriendlyProjectile>();
        p.ProjectileStats = projectileStats;

        currentAmmo--; //Update the ammo acount
    }

    public void FireEnemyProjectile(Vector3 _direction, Vector3 _spawnPosition) {
        GameObject projectile = CreateProjectile(_direction, _spawnPosition);

        //Add the projectile component
        EnemyProjectile p = projectile.AddComponent<EnemyProjectile>();
        p.ProjectileStats = projectileStats;
    }

    public void FireEnemyProjectile(Vector3 _direction, Vector3 _spawnPosition, float _time) {
        GameObject projectile = CreateProjectile(_direction, _spawnPosition);

        //Add the projectile component
        EnemyProjectile p = projectile.AddComponent<EnemyProjectile>();
        p.ProjectileStats = projectileStats;

        StartCoroutine(DestroyAfterSeconds(projectile, _time));
    }

    private IEnumerator DestroyAfterSeconds(GameObject _projectile, float _seconds) {
        yield return new WaitForSeconds(_seconds);
        Destroy(_projectile);
    }

    public void FireEnemyProjectile(Vector3 _direction, Vector3 _spawnPosition, float _angle, float _coneRadius, int _amount) {
        for (int i = 0; i < _amount; i++) {
            GameObject projectile = CreateProjectile(_direction, _spawnPosition);
            
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            float randomness = Random.Range(_angle - _coneRadius, _angle + _coneRadius);
            projectile.transform.rotation = Quaternion.AngleAxis((angle + randomness) - 90f, Vector3.forward);

            EnemyProjectile p = projectile.AddComponent<EnemyProjectile>();
            p.ProjectileStats = projectileStats;
        }
    }

    private GameObject CreateProjectile(Vector3 _direction, Vector3 _spawnPosition) {
        //The spawn position will be dependant of the direction the player is aiming
        Vector3 spawnPosition = new Vector3(_spawnPosition.x, _spawnPosition.y, _spawnPosition.z) + _direction * projectileStats.projectileSpawnDistance;
        spawnPosition.z = -1;

        //Instantiate a bullet using the information from the player's projectile scriptableobject
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(_direction));

        //Calculate the rotation of the bullet
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        return projectile;
    }

    public void AddAmmo(int _ammoAMount) {
        currentAmmo += _ammoAMount;
        GameObject.Find("AmmoCount").GetComponent<Text>().text = currentAmmo.ToString();
    }
}
