using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
    private int width;
    private int height;
    private int cellSize;
    private Vector2 origin;
    private int[,] gridArray;

    public Grid(int _width, int _height, int _cellSize, Vector2 _origin) {
        this.width = _width;
        this.height = _height;
        this.cellSize = _cellSize;
        this.origin = _origin;
        gridArray = new int[width, height];

        CreateGrid();
    }

    private void CreateGrid() {
        //Cycle through the multi-dimensional array
        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.red, 100);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, 100);
            }
        }
    }

    private Vector2 GetWorldPosition(int _x, int _y) {
        return new Vector2(_x, _y) * cellSize + origin;
    }
}
