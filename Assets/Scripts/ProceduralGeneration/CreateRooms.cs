using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRooms : MonoBehaviour {
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private int cellSize;
    [SerializeField] private int numberOfRooms;
    [SerializeField] private List<GameObject> roomPrefabs;

    public int CellSize { get { return cellSize; } }

    private void Start() {
        //CreateAllRooms();
        //GenerateDungeon();
        DepthFirstSearch();
    }

    private void GenerateDungeon() {
        //TODO:
        //SPAWN ROOM
        bool[] firstRoomExits = new bool[4];
        firstRoomExits[0] = false; firstRoomExits[1] = false; firstRoomExits[2] = true; firstRoomExits[3] = true;
        new Room(0, 0, firstRoomExits, this);
        //GET FIRST AVAILABLE ROOM WITH UNCONNECTED OPENINGS
        //TELL ROOM TO SPAWN ADJACENT ROOMS
        //WAIT UNTIL ROOMS SPAWNED
        //ADD CREATED ROOMS TO DUNGEON
        //REPEAT UNTIL NO UNCONNECTED OPENINGS
    }

    private void CreateAllRooms() {
        Vector2 position = new Vector2(0, 0);
        foreach (GameObject room in roomPrefabs) {
            SpawnRoom(room, position);

            position.x += cellSize;
            if (position.x == cellSize * gridWidth) {
                position.x = 0;
                position.y += cellSize;
            }
        }
    }

    private void DepthFirstSearch() {
        //PERFORM DEPTH FIRST SEARCH TO CREATE DUNGEON LAYOUT

        GenerateDungeon();
    }

    public void SpawnRoom(int _roomType, Vector2 _position) {
        GameObject roomObject = Instantiate(roomPrefabs[_roomType-1]);
        roomObject.transform.position = new Vector3(_position.x, _position.y, 100);
    }

    public void SpawnRoom(GameObject _room, Vector2 _position) {
        GameObject roomObject = Instantiate(_room);
        roomObject.transform.position = new Vector3(_position.x, _position.y, 100); ;
    }
}
