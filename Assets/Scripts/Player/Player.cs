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
    
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    public float MaxMovementSpeed { get { return maxMovementSpeed; } }
    public float Health { get { return health; } }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() {
        AddMovementForce();
    }

    private void AddMovementForce() {
        //Calculate the force for the player movement, using it's acceleration to reach the maximum speed
        Vector2 force = playerMovement.CalculateForce(rb.velocity, maxMovementSpeed, acceleration, deceleration);
        rb.AddForce(force, ForceMode2D.Force);
    }
}
