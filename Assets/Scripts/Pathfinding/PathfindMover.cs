using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindMover : MonoBehaviour {

    private const float speed = 40f;
    private int currentPathIndex;
    private List<Vector2> pathVectorList;
    [SerializeField] private Transform target;
    private Pathfinding pathfinding;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    protected Vector2 moveDirection;
    protected Rigidbody2D rb;

    private void Start() {
        target = GameObject.Find("Mover").transform;
        pathfinding = GameObject.Find("Grid").GetComponent<GridTest>().pathfinding;

        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        SetTargetPosition(target.position);
        InvokeRepeating("FindPath", 0, 5f);
    }

    private void FixedUpdate() {
        Move(2, moveDirection);
    }

    private void FindPath() {
        HandleMovement();
        SetTargetPosition(target.position);
    }

    private void HandleMovement() {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            Debug.Log(Vector2.Distance(transform.position, targetPosition));
            if (Vector2.Distance(transform.position, targetPosition) > .01f) {
                moveDirection = (targetPosition - transform.position).normalized;
            } else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving() {
        moveDirection = Vector2.zero;
        pathVectorList = null;
    }

    public Vector2 GetPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector2 targetPosition) {
        currentPathIndex = 0;
        pathVectorList = pathfinding.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
        }
    }

    protected void Move(float _speed, Vector2 _direction) {
        Vector2 speed = new Vector2(_direction.x * _speed, _direction.y * _speed);
        speed.Normalize();

        bool succes = CheckToMove(_direction, _speed); //Try to move, move if there is no collision
        if (!succes) { //If there is no collision, allowing the player to move...
            succes = CheckToMove(new Vector2(_direction.x, 0), _speed); //Try to move left/right
            if (!succes) { //If we can't move left/right, try to move up/down
                succes = CheckToMove(new Vector2(0, _direction.y), _speed);
            }
        }
    }

    private bool CheckToMove(Vector2 _direction, float _speed) {
        //Check for potential collisions
        int count = rb.Cast(_direction, movementFilter, castCollisions, _speed * Time.fixedDeltaTime + 1);

        if (count == 0) { //No collision detected
            Vector2 moveVector = _direction.normalized * _speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        return false; //If a collision has been detected, return false
    }

}