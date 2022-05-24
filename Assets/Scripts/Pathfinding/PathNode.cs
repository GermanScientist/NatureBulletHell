using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {
    private Grid<PathNode> grid;
    private int x;
    private int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode previousNode;

    public int X { get { return x; } }
    public int Y { get { return y; } }

    public PathNode(Grid<PathNode> _grid, int _x, int _y) {
        this.grid = _grid;
        this.x = _x;
        this.y = _y;
    }

    public void CalculateFCost() {
        fCost = gCost + hCost;
    }

    public override string ToString() {
        return x + "," + y;
    }
}
