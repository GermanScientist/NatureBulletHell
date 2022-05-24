using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid<GridObject> {
    private int width;
    private int height;
    private int cellSize;
    private Vector2 origin;
    private GridObject[,] gridArray;

    public int Width { get { return width; } }
    public int Height { get { return height; } }
    public int CellSize { get { return cellSize; } }

    public Grid(int _width, int _height, int _cellSize, Vector2 _origin, 
            Func<Grid<GridObject>, int, int, GridObject> _createGridObject) {
        this.width = _width;
        this.height = _height;
        this.cellSize = _cellSize;
        this.origin = _origin;
        gridArray = new GridObject[width, height];

        CreateGrid(_createGridObject);
    }

    private void CreateGrid(Func<Grid<GridObject>, int, int, GridObject> _createGridObject) { //Passes in a function returning the grid, x and y
        //Cycle through the multi-dimensional array
        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                gridArray[x, y] = _createGridObject(this, x, y);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.red, 100);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, 100);
            }
        }
    }

    private Vector2 GetWorldPosition(int _x, int _y) {
        return new Vector2(_x, _y) * cellSize + origin;
    }

    public GridObject GetGridObject(int _x, int _y) {
        if (_x >= 0 && _y >= 0 && _x < width && _y < height) 
            return gridArray[_x, _y];
        return default(GridObject);
    }
}
