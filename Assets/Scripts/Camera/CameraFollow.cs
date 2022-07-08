using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = .25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private Player player;

    private void Start() {
        target = GameObject.Find("Player").transform;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update() {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, 
            targetPosition + (player.MouseDirection*4), ref velocity, smoothTime);
    }
}
