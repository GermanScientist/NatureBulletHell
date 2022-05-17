using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New bullet", menuName = "ScriptableObjects/Bullet", order = 1)]
public class WeaponSO : ScriptableObject {
    public int maxAmmo = 20;
    public int projectileDamage = 1;
    public float projectileSpeed = 5;
    public float projectileSpawnDistance = 3f;
}
