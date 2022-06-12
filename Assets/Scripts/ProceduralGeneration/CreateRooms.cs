using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRooms : MonoBehaviour {
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private int numberOfRooms;
    [SerializeField] private List<GameObject> rooms;

    private void Start() {
        CreateAllRooms();
    }

    private void GenerateRooms() {
        //TODO:
        //SPAWN ROOM
        GameObject roomGO = Instantiate(rooms[15 - 1]);
        roomGO.transform.position = new Vector3(0, 0, 100);
        //GET FIRST AVAILABLE ROOM WITH UNCONNECTED OPENINGS
        //TELL ROOM TO SPAWN ADJACENT ROOMS
        //WAIT UNTIL ROOMS SPAWNED
        //ADD CREATED ROOMS TO DUNGEON
        //REPEAT UNTIL NO UNCONNECTED OPENINGS
    }

    private void CreateAllRooms() {
        Vector3 position = new Vector3(0, 0, 100);
        foreach (GameObject room in rooms) {
            GameObject roomObject = Instantiate(room);
            roomObject.transform.position = position;

            position.x += 70;
            if (position.x == 70 * 5) {
                position.x = 0;
                position.y += 70;
            }
        }
    }
}
