using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    private int x;
    private int y;
    private bool[] exits; //Top - bottom - left - right
    private CreateRooms roomGenerator;

    public int X { get { return x; } }
    public int Y { get { return y; } }
    public bool[] Exits { get { return exits; } }
    public bool Visited;

    public Room(int _x, int _y, bool[] _exits, CreateRooms _roomGenerator) {
        this.x = _x;
        this.y = _y;
        this.exits = _exits;
        this.roomGenerator = _roomGenerator;
    }

    public void Spawn() {
        int roomType = 0;
        if (exits[0]) roomType += 1;
        if (exits[1]) roomType += 2;
        if (exits[2]) roomType += 4;
        if (exits[3]) roomType += 8;
        roomGenerator.SpawnRoom(roomType, new Vector2(x * roomGenerator.CellSize, y * roomGenerator.CellSize));
    }
}
