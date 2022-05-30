using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding {
    private const int STRAIGHTCOST = 10;
    private const int DIAGONALCOST = 14;
    private Vector2 origin;

    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public Pathfinding(int _width, int _height, int _cellSize, Vector2 _origin) {

        grid = new Grid<PathNode>(_width, _height, _cellSize, _origin,
            (Grid<PathNode> _grid, int _x, int _y) => new PathNode(_grid, _x, _y));
    }

    private List<PathNode> FindPath(int _startX, int _startY, int _endX, int _endY) {
        PathNode startNode = grid.GetGridObject(_startX, _startY);
        PathNode endNode = grid.GetGridObject(_endX, _endY);
        Debug.Log("s+e node" + startNode + " , " + endNode);

        if(startNode == null || endNode == null) {
            return null;
        }
        
        openList = new List<PathNode> { startNode }; //Add startNode to openList
        closedList = new List<PathNode>(); //closedList remains empty

        for (int x = 0; x < grid.Width; x++) {
            for (int y = 0; y < grid.Height; y++) {
                PathNode currentNode = grid.GetGridObject(x, y);
                currentNode.gCost = 999999;
                currentNode.CalculateFCost();
                currentNode.previousNode = null; //Make the the node doesn't contain any data from the previous path
            }
        }

        startNode.gCost = 0; //Since this is the start node, the gCost is automatically zero
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        //Cycle through the open list
        while(openList.Count > 0) {
            PathNode currentNode = GetLowestFcostNode(openList); //The current node will be the node with the lowest cost

            //First we check whether this is the final node
            if(currentNode == endNode) return CalculatePath(endNode);

            //Otherwise we move on
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //Cycle through neighbors
            foreach (PathNode neighborNode in GetNeighborList(currentNode)) {
                if (closedList.Contains(neighborNode)) continue; //If the neighborNode is already on the closed list, we have already searched it

                //The tentative cost is based on the current node's cost and the movement cost to go from the current node to the neighbor node
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighborNode);

                //Check if we have a better path, if so, update it
                if(tentativeGCost < neighborNode.gCost) {
                    neighborNode.previousNode = currentNode;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalculateDistanceCost(neighborNode, endNode);
                    neighborNode.CalculateFCost();

                    if (!openList.Contains(neighborNode)) openList.Add(neighborNode);
                }
            }
        }

        //No more nodes in the open list
        return null;
    }

    public List<Vector2> FindPath(Vector2 startWorldPosition, Vector2 endWorldPosition) {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);
        Debug.Log("Step1");

        List<PathNode> path = FindPath(startX, startY, endX, endY);
        Debug.Log(startX + " , " + startY + " , " + endX + " , " + endY);
        if (path == null) {
            Debug.Log("Path not found");
            return null;
        } else {
            Debug.Log("Path found");
            List<Vector2> vectorPath = new List<Vector2>();
            foreach (PathNode pathNode in path) {
                vectorPath.Add(new Vector2(pathNode.X, pathNode.Y) * grid.CellSize + Vector2.one * grid.CellSize * .5f);
            }
            return vectorPath;
        }
    }

    private List<PathNode> GetNeighborList(PathNode _currentNode) {
        List<PathNode> neighbourList = new List<PathNode>();

        if (_currentNode.X - 1 >= 0) { 
            neighbourList.Add(GetNode(_currentNode.X - 1, _currentNode.Y)); //Get the node on the left
            if (_currentNode.Y - 1 >= 0) neighbourList.Add(GetNode(_currentNode.X - 1, _currentNode.Y - 1)); //Get the node down-left
            if (_currentNode.Y + 1 < grid.Height) neighbourList.Add(GetNode(_currentNode.X - 1, _currentNode.Y + 1)); //Get the node top-left
        }
        if (_currentNode.X + 1 < grid.Width) { 
            neighbourList.Add(GetNode(_currentNode.X + 1, _currentNode.Y)); //Get the node on the right
            if (_currentNode.Y - 1 >= 0) neighbourList.Add(GetNode(_currentNode.X + 1, _currentNode.Y - 1)); //Get the node down-right
            if (_currentNode.Y + 1 < grid.Height) neighbourList.Add(GetNode(_currentNode.X + 1, _currentNode.Y + 1)); //Get the node top-right
        }
        
        if (_currentNode.Y - 1 >= 0) neighbourList.Add(GetNode(_currentNode.X, _currentNode.Y - 1)); //Get the node down
        if (_currentNode.Y + 1 < grid.Height) neighbourList.Add(GetNode(_currentNode.X, _currentNode.Y + 1)); //Get the node top

        return neighbourList;
    }

    public PathNode GetNode(int x, int y) {
        return grid.GetGridObject(x, y);
    }

    private List<PathNode> CalculatePath(PathNode _endNode) {
        List<PathNode> path = new List<PathNode>();
        path.Add(_endNode); //The path starts at the end node

        PathNode currentNode = _endNode;
        while(currentNode.previousNode != null) { //While the previous node has a parent create the path
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }

        path.Reverse(); //Reverse the path, since it currently starts at the end
        
        return path;
    }

    private int CalculateDistanceCost(PathNode _pathA, PathNode _pathB) {
        int xDistance = Mathf.Abs(_pathA.X - _pathB.X);
        int yDistance = Mathf.Abs(_pathA.Y - _pathB.Y);
        int remainingDistance = Mathf.Abs(xDistance - yDistance);

        int costDiagonally = DIAGONALCOST * Mathf.Min(xDistance, yDistance);
        int costStraight = STRAIGHTCOST * remainingDistance;

        return costDiagonally + costStraight;
    }

    private PathNode GetLowestFcostNode(List<PathNode> _pathNodeList) {
        PathNode lowestFcostNode = _pathNodeList[0];
        foreach (PathNode pathNode in _pathNodeList)
            if (pathNode.fCost < lowestFcostNode.fCost) 
                lowestFcostNode = pathNode;
        
        return lowestFcostNode;
    }
}
