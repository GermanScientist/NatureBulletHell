using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRooms : MonoBehaviour {
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private int cellSize;
    [SerializeField] private int numberOfRooms;
    [SerializeField] private List<GameObject> roomPrefabs;
    private List<Room> board;

    public int CellSize { get { return cellSize; } }

    private void Start() {
        //CreateAllRooms();
        //GenerateDungeon();
        DepthFirstSearch();
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

    private void GenerateDungeon() {
        for (int i = 0; i < gridWidth; i++) {
            for (int j = 0; j < gridHeight; j++) {
                Room currentCell = board[(i + j * gridWidth)];
                if (currentCell.Visited) {
                    currentCell.Spawn();
                }
            }
        }
    }

    private void DepthFirstSearch() {
        board = new List<Room>();

        for (int i = 0; i < gridWidth; i++)
            for (int j = 0; j < gridHeight; j++)
                board.Add(new Room(i, j, new bool[4], this));

        int currentRoomIndex = 0;
        int iteration = 0;

        Stack<int> path = new Stack<int>();

        while (iteration++ < 1000) {
            board[currentRoomIndex].Visited = true;

            if (currentRoomIndex == board.Count - 1)  break;

            //Find the neighbors
            List<int> neighbors = CheckNeighbors(currentRoomIndex);

            if (neighbors.Count == 0) {
                if (path.Count == 0) break;
                currentRoomIndex = path.Pop();
            } else {
                path.Push(currentRoomIndex);

                int newRoomIndex = neighbors[Random.Range(0, neighbors.Count)];

                if (newRoomIndex > currentRoomIndex) { //Path either goes down or right
                    if (newRoomIndex - 1 == currentRoomIndex) {//Path goes down
                        board[currentRoomIndex].Exits[0] = true;
                        currentRoomIndex = newRoomIndex;
                        board[currentRoomIndex].Exits[1] = true;
                    } else { //Path goes right
                        board[currentRoomIndex].Exits[3] = true;
                        currentRoomIndex = newRoomIndex;
                        board[currentRoomIndex].Exits[2] = true;
                    }
                } else {
                    if (newRoomIndex + 1 == currentRoomIndex) { //Path goes up
                        board[currentRoomIndex].Exits[1] = true;
                        currentRoomIndex = newRoomIndex;
                        board[currentRoomIndex].Exits[0] = true;
                    } else { //Path goes left
                        board[currentRoomIndex].Exits[2] = true;
                        currentRoomIndex = newRoomIndex;
                        board[currentRoomIndex].Exits[3] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell) {
        List<int> neighbors = new List<int>();

        //check up neighbor
        if (cell - gridWidth >= 0 && !board[(cell - gridWidth)].Visited)
            neighbors.Add((cell - gridWidth));

        //check down neighbor
        if (cell + gridWidth < board.Count && !board[(cell + gridWidth)].Visited)
            neighbors.Add((cell + gridWidth));

        //check right neighbor
        if ((cell + 1) % gridWidth != 0 && !board[(cell + 1)].Visited)
            neighbors.Add((cell + 1));

        //check left neighbor
        if (cell % gridWidth != 0 && !board[(cell - 1)].Visited) 
            neighbors.Add((cell - 1));

        return neighbors;
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
