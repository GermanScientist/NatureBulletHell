using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	private Vector2[] path;
	private int targetIndex;
	private Vector2 direction;
	private Enemy enemy;

	[SerializeField] private Transform target;

	private void Start() {
		target = GameObject.Find("Player").transform;
		enemy = GetComponent<Enemy>();
		InvokeRepeating("ChaseTarget", 0, .5f);
	}

	private void ChaseTarget() {
		if(target == null) target = GameObject.Find("Player").transform;
		if (enemy != null) enemy.CanMove = false;

		if (Vector2.Distance(transform.position, target.position) < enemy.ChaseDistance) {
			path = Pathfinding.RequestPath(transform.position, target.position);
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	private IEnumerator FollowPath() {
		Vector2 currentWaypoint = path[0];

		while (true) {
			if ((Vector2)transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					targetIndex = 0;
					path = new Vector2[0];
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			if(enemy != null) {
				enemy.Target = currentWaypoint;
				enemy.CanMove = true;
			}

			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
