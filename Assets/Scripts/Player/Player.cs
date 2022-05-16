using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    [SerializeField] private int maxHitpoints = 100;
    [SerializeField] private float maxMovementSpeed = 5f;
    [SerializeField] private float acceleration = 0.01f;
    [SerializeField] private float deceleration = 2f;
    
    private Weapon weapon;
    
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    private Vector3 mousePosition;
    private Vector3 mouseDirection;

    private int currentHitpoints;

    public int Hitpoints {  get { return currentHitpoints; }  }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();

        Cursor.lockState = CursorLockMode.Confined;
        weapon = GetComponent<Weapon>();
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
        if (weapon.CurrentAmmo <= 0) return;
        if (!Input.GetMouseButtonDown(0)) return;
       
        weapon.FireProjectile(mouseDirection); //Only fire a projectile when the player has ammo and the mousebutton has been pressed
    }

    public void Damage(int _damageAmount) {
        currentHitpoints -= _damageAmount;
    }
}
