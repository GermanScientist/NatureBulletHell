using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRooms : MonoBehaviour {
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private int cellSize;
    [SerializeField] private int numberOfRooms;
    [SerializeField] private List<GameObject> roomPrefabs;
    private List<Room> grid;

    public int CellSize { get { return cellSize; } }

    private void Start() {
        DepthFirstSearch(); //Generate the room using depth first search
    }

    //Generate the dungeon using the grid's width and height
    //Spawn the rooms that have been visited during the depth first search
    private void GenerateDungeon() {
        for (int i = 0; i < gridWidth; i++) {
            for (int j = 0; j < gridHeight; j++) {
                Room currentRoom = grid[(i + j * gridWidth)];
                if (currentRoom.Visited) currentRoom.Spawn();
            }
        }
    }

    //Create a grid that holds the rooms
    private void CreateGrid() {
        grid = new List<Room>();

        for (int i = 0; i < gridWidth; i++)
            for (int j = 0; j < gridHeight; j++)
                grid.Add(new Room(i, j, new bool[4], this));
    }

    //Set the exit points of the room and the room next to it
    private void SetExits(ref int _currentIndex, int _newIndex, int _in, int _out) {
        grid[_currentIndex].Exits[_in] = true;
        _currentIndex = _newIndex;
        grid[_currentIndex].Exits[_out] = true;
    }

    //Find where the neighboring room is from the current room, and create the correct exits
    private void FindAndSetExits(List<int> _neighbors, ref int _currentRoomIndex) {
        int newRoomIndex = _neighbors[Random.Range(0, _neighbors.Count)];

        if (newRoomIndex > _currentRoomIndex) { //Path either goes down or right
            if (newRoomIndex - 1 == _currentRoomIndex)
                SetExits(ref _currentRoomIndex, newRoomIndex, 0, 1); //Path goes down
            else
                SetExits(ref _currentRoomIndex, newRoomIndex, 3, 2); //Path goes right
        } else {
            if (newRoomIndex + 1 == _currentRoomIndex)
                SetExits(ref _currentRoomIndex, newRoomIndex, 1, 0); //Path goes up
            else
                SetExits(ref _currentRoomIndex, newRoomIndex, 2, 3); //Path goes left
        }
    }

    //Iterate through all the rooms to create the path of rooms
    //This method will be responsible for creating the layout of the map
    private void IterateThroughRooms() {
        int currentRoomIndex = 0;
        int iteration = 0;

        Stack<int> path = new Stack<int>(); //The list that will hold the path of room indices

        while (++iteration < 1000) {
            grid[currentRoomIndex].Visited = true; //This cell has now been visisted, so make sure the room knows it

            if (currentRoomIndex == grid.Count - 1) break; //Stop iterating when the current room index is bigger than the grid size

            List<int> neighbors = CheckNeighbors(currentRoomIndex); //Find the neighbors using the current room index

            //If there are no neighbors, stop iterating
            if (neighbors.Count == 0) {
                if (path.Count != 0) 
                    currentRoomIndex = path.Pop();
                break;
            }
            
            path.Push(currentRoomIndex); //Add the room to the path

            FindAndSetExits(neighbors, ref currentRoomIndex); //Find and set the exits for the rooms
        }
    }

    //Perform depth first search
    private void DepthFirstSearch() {
        CreateGrid();
        IterateThroughRooms(); //This method will take care of the layout and the types of rooms that will spawn
        GenerateDungeon(); //This method actually spawns them using the information from the previous method
    }

    //Check whether the cell has any neighbors that hasn't been visited yet
    List<int> CheckNeighbors(int cell) {
        List<int> neighbors = new List<int>();

        //Check the neighbor above this cell
        if (cell - gridWidth >= 0 && !grid[(cell - gridWidth)].Visited)
            neighbors.Add((cell - gridWidth));

        //Check the neighbor below this cell
        if (cell + gridWidth < grid.Count && !grid[(cell + gridWidth)].Visited)
            neighbors.Add((cell + gridWidth));

        //Check the neighbor to the right of this cell
        if ((cell + 1) % gridWidth != 0 && !grid[(cell + 1)].Visited)
            neighbors.Add((cell + 1));

        //Check the neighbor to the left of this cell
        if (cell % gridWidth != 0 && !grid[(cell - 1)].Visited) 
            neighbors.Add((cell - 1));

        return neighbors; //Add any neighbors that have been found
    }

    //Spawn the room based on the roomtype and position
    public void SpawnRoom(int _roomType, Vector2 _position) {
        GameObject roomObject = Instantiate(roomPrefabs[_roomType-1]);
        roomObject.transform.position = new Vector3(_position.x, _position.y, 100);
    }
}
