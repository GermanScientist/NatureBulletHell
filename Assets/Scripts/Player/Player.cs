using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    private int health;
    [SerializeField] private float maxMovementSpeed = 5f;
    [SerializeField] private float acceleration = 0.01f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private float bulletSpawnDistance = 3f;
    [SerializeField] private float bulletSpeed = 5f;
    
    [SerializeField] private GameObject bulletPrefab;
    
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    private Vector3 mousePosition;
    private Vector3 mouseDirection;

    public float MaxMovementSpeed { get { return maxMovementSpeed; } }
    public float Health { get { return health; } }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update() {
        GetAimDirection();
        CheckToFire();
    }

    private void FixedUpdate() {
        AddMovementForce();
    }

    private void AddMovementForce() {
        //Calculate the force for the player movement, using it's acceleration to reach the maximum speed
        Vector2 force = playerMovement.CalculateForce(rb.velocity, maxMovementSpeed, acceleration, deceleration);
        rb.AddForce(force, ForceMode2D.Force);
    }

    private void GetAimDirection() {
        mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        mouseDirection = (mousePosition - this.transform.position).normalized;
    }

    private void CheckToFire() {
        if (Input.GetMouseButtonDown(0)) {
            //TODO:
                //Check for ammo
            FireProjectile();
        }
    }

    private void FireProjectile() {
        Vector3 pos = transform.position;
        GameObject projectile = Instantiate(bulletPrefab, new Vector3(pos.x, pos.y, -1) + mouseDirection*bulletSpawnDistance, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.AddComponent<Rigidbody2D>();
        projectileRigidbody.gravityScale = 0;
        projectileRigidbody.velocity = mouseDirection.normalized * bulletSpeed;
    }
}
