using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {
	
	private bool walkable;
	private int gridX;
	private int gridY;

	private int gCost;
	private int hCost;
	private int heapIndex;

	private Vector2 worldPosition;
	private Node parent;

	public Vector2 GridVector { get { return new Vector2(gridX, gridY); } }
	public Vector2 WorldPosition { get { return worldPosition; } }
	public bool Walkable { get { return walkable; } }
	public int HCost { set { hCost = value; } }
	public int fCost { get { return gCost + hCost; } }

	public int GCost { 
		get { return gCost; } 
		set { gCost = value; } 
	}

	public int HeapIndex {
		get { return heapIndex; }
		set { heapIndex = value; }
	}

	public Node Parent { 
		get { return parent; } 
		set { parent = value; } 
	}

	public Node(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int CompareTo(Node _nodeToCompare) {
		int compare = fCost.CompareTo(_nodeToCompare.fCost);
		if (compare == 0) compare = hCost.CompareTo(_nodeToCompare.hCost);
		
		return -compare;
	}
}
