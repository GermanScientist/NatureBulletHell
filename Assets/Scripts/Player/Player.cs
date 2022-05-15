using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    //Fields
    [SerializeField] private int maxHitpoints = 100;
    [SerializeField] private int currentHitpoints;

    [SerializeField] private int maxAmmo = 20;
    [SerializeField] private int currentAmmo;

    [SerializeField] private float maxMovementSpeed = 5f;
    [SerializeField] private float acceleration = 0.01f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private Projectile projectile;
    
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    private Vector3 mousePosition;
    private Vector3 mouseDirection;

    //Properties
    public int Hitpoints { 
        get { return currentHitpoints; } 
    }

    //Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();

        Cursor.lockState = CursorLockMode.Confined;

        currentAmmo = maxAmmo;
    }

    private void Update() {
        GetAimDirection();
        CheckToFire();
    }

    private void FixedUpdate() {
        AddMovementForce();
    }

    private void AddMovementForce() {
        //Calculate the force for the desired movement speed
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
        if (!Input.GetMouseButtonDown(0)) return;
        if (currentAmmo <= 0) return;
        FireProjectile(); //Only fire a projectile when the player has ammo and the mousebutton has been pressed
        currentAmmo--;
    }

    private void FireProjectile() {
        //The spawn position will be dependant of the direction the player is aiming
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, -1) + mouseDirection * projectile.bulletSpawnDistance;

        //Instantiate a bullet using the information from the player's projectile scriptableobject
        GameObject bullet = Instantiate(projectile.prefab, spawnPosition, Quaternion.LookRotation(mouseDirection));
        
        //Remove the bullet's gravity and shoot the bullet towards the direction of the mouse when the bullet was fired
        Rigidbody2D projectileRigidbody = bullet.AddComponent<Rigidbody2D>();
        projectileRigidbody.gravityScale = 0;
        projectileRigidbody.velocity = bullet.transform.forward * projectile.speed;
    }

    public void DamagePlayer(int _damageAmount) {
        currentHitpoints -= _damageAmount;
    }
}
