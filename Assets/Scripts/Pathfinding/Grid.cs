using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public bool displayGridGizmos;

	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;

	private Node[,] grid;
	private float nodeDiameter;
	private int gridSizeX, gridSizeY;

	public int MaxSize { get { return gridSizeX * gridSizeY; } }

	void Awake() {
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);

		CreateGrid();
	}

	private void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x/2 - Vector2.up * gridWorldSize.y/2;

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
				bool walkable = (Physics2D.OverlapCircle(worldPoint,nodeRadius,unwalkableMask) == null); // if no collider2D is returned by overlap circle, then this node is walkable

				grid[x,y] = new Node(walkable,worldPoint, x,y);
			}
		}
	}

	public List<Node> GetNeighbours(Node _node, int _depth = 1) {
		List<Node> neighbours = new List<Node>();

		for (int x = -_depth; x <= _depth; x++) {
			for (int y = -_depth; y <= _depth; y++) {
				if (x == 0 && y == 0) continue;

				int checkX = (int)_node.GridVector.x + x;
				int checkY = (int)_node.GridVector.y + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
					neighbours.Add(grid[checkX,checkY]);
			}
		}

		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector2 _worldPosition) {
		float percentX = ((_worldPosition - (Vector2)transform.position).x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = ((_worldPosition - (Vector2)transform.position).y + gridWorldSize.y/2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];
	}
	
	private void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector2(gridWorldSize.x,gridWorldSize.y));
		if (grid != null && displayGridGizmos) {
			foreach (Node node in grid) {
				Gizmos.color = Color.red;
				if (node.Walkable)
					Gizmos.color = Color.white;

				Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeDiameter-.1f));
			}
		}
	}

}