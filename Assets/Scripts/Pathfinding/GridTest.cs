using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour {
    [SerializeField] private int x, y, w;
    [SerializeField] private Vector2 origin;
    [SerializeField] private PathfindMover pathfindMover;
    [SerializeField] private Transform pathfindTarget;
    public Pathfinding pathfinding;

    private void Awake() {
        pathfinding = new Pathfinding(x, y, w, origin);
    }

    private void Update() {
        //pathfindMover.SetTargetPosition(pathfindTarget.position);
    }
}
