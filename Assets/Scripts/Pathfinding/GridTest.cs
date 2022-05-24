using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour {
    [SerializeField] int x, y, w;
    [SerializeField] Vector2 origin;
    
    private void Start() {
        Pathfinding pathfinding = new Pathfinding(x, y, w, origin);
    }
}
