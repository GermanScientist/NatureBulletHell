using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New bullet", menuName = "ScriptableObjects/Bullet", order = 1)]
public class Projectile : ScriptableObject {
    public GameObject prefab;
    public float speed = 5;
    public float damage = 1;
    public float bulletSpawnDistance = 3f;
}
