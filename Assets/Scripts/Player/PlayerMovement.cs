using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Vector2 direction;
    private Rigidbody2D rb;

    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        UpdatePlayerDirection();
    }

    private void UpdatePlayerDirection() {
        float horizontalDirection = Input.GetAxisRaw("Horizontal"); //-1 is left, 1 is right
        float verticalDirection = Input.GetAxisRaw("Vertical"); //-1 is down, 1 is up

        direction = new Vector2(horizontalDirection, verticalDirection);
    }

    public void MovePlayer(float _speed) {
        Vector2 speed = new Vector2(direction.x * _speed, direction.y * _speed);
        speed.Normalize();

        bool succes = CheckToMovePlayer(direction, _speed); //Try to move, move if there is no collision
        if(!succes) { //If there is no collision, allowing the player to move...
            succes = CheckToMovePlayer(new Vector2(direction.x, 0), _speed); //Try to move left/right
            if(!succes) { //If we can't move left/right, try to move up/down
                succes = CheckToMovePlayer(new Vector2(0, direction.y), _speed);
            }
        }
    }

    private bool CheckToMovePlayer(Vector2 _direction, float _speed) {
        //Check for potential collisions
        int count = rb.Cast(_direction, movementFilter, castCollisions, _speed * Time.fixedDeltaTime + collisionOffset);
        
        if(count == 0) { //No collision detected
            Vector2 moveVector = _direction.normalized * _speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        return false; //If a collision has been detected, return false
    }
}
