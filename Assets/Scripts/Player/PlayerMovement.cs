using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Vector2 direction;

    private void Update() {
        UpdatePlayerDirection();
    }

    private void UpdatePlayerDirection() {
        //Gives a value between -1 and 1
        float horizontalDirection = Input.GetAxisRaw("Horizontal"); //-1 is left
        float verticalDirection = Input.GetAxisRaw("Vertical"); //-1 is down
        
        direction = new Vector2(horizontalDirection, verticalDirection);
    }

    public Vector2 CalculateForce(Vector2 _currentVelocity, float _speed, float _acceleration, float _deceleration) {
        Vector2 desiredVelocity = transform.rotation * direction * _speed; //Calculate the desiredVelocity
        Vector2 force = (desiredVelocity - _currentVelocity*_deceleration) * _acceleration; //Calculate the force the player requires to reach it's desired velocity
        
        return force.normalized; //Return the normalized force, to make sure the player doesn't move faster diagonally
    }
}
