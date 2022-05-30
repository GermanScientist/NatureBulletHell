using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour {

	private Grid grid;
	private static Pathfinding instance;
	
	private void Awake() {
		grid = GetComponent<Grid>();
		instance = this;
	}

	public static Vector2[] RequestPath(Vector2 _fromPath, Vector2 _toPath) {
		return instance.FindPath(_fromPath, _toPath);
	}
	
	private Vector2[] FindPath(Vector2 _fromPath, Vector2 _toPath) {
		Stopwatch sw = new Stopwatch();
		sw.Start();
		
		Vector2[] waypoints = new Vector2[0];
		bool pathSuccess = false;
		
		Node startNode = grid.NodeFromWorldPoint(_fromPath);
		Node targetNode = grid.NodeFromWorldPoint(_toPath);
		startNode.Parent = startNode;
		
		if (startNode.Walkable && targetNode.Walkable) {
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);
				
				if (currentNode == targetNode) {
					sw.Stop();
					print ("Path found: " + sw.ElapsedMilliseconds + " ms");
					pathSuccess = true;
					break;
				}
				
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.Walkable || closedSet.Contains(neighbour)) continue;
					
					int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour)+TurningCost(currentNode, neighbour);
					if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour)) {
						neighbour.GCost = newMovementCostToNeighbour;
						neighbour.HCost = GetDistance(neighbour, targetNode);
						neighbour.Parent = currentNode;
						
						if (!openSet.Contains(neighbour)) 
							openSet.Add(neighbour);
						else 
							openSet.UpdateItem(neighbour);
					}
				}
			}
		}

		if (pathSuccess)
			waypoints = RetracePath(startNode,targetNode);

		return waypoints;
	}

	private int TurningCost(Node _fromPath, Node _toPath) {
		return 0;
		
		Vector2 dirOld = new Vector2(_fromPath.GridVector.x - _fromPath.Parent.GridVector.x, _fromPath.GridVector.y - _fromPath.Parent.GridVector.y);
		Vector2 dirNew = new Vector2(_toPath.GridVector.x - _fromPath.GridVector.x, _toPath.GridVector.y - _fromPath.GridVector.y);
		
		if (dirNew == dirOld) return 0;
		else if (dirOld.x != 0 && dirOld.y != 0 && dirNew.x != 0 && dirNew.y != 0) return 5;
		else return 10;
	}
	
	private Vector2[] RetracePath(Node _startNode, Node _endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = _endNode;
		
		while (currentNode != _startNode) {
			path.Add(currentNode);
			currentNode = currentNode.Parent;
		}
		Vector2[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
		
	}
	
	private Vector2[] SimplifyPath(List<Node> _path) {
		List<Vector2> waypoints = new List<Vector2>();
		Vector2 directionOld = Vector2.zero;
		
		for (int i = 1; i < _path.Count; i ++) {
			Vector2 directionNew = new Vector2(_path[i-1].GridVector.x - _path[i].GridVector.x,
				_path[i-1].GridVector.y - _path[i].GridVector.y);
			
			if (directionNew != directionOld) waypoints.Add(_path[i].WorldPosition);
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}
	
	private int GetDistance(Node _nodeA, Node _nodeB) {
		int dstX = Mathf.Abs((int)_nodeA.GridVector.x - (int)_nodeB.GridVector.x);
		int dstY = Mathf.Abs((int)_nodeA.GridVector.y - (int)_nodeB.GridVector.y);
		
		if (dstX > dstY) return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
}
