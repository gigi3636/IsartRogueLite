using Godot;
using System;
using System.Collections.Generic;

public partial class DungeonSpawner : Node2D
{
    [Export] private CurrentDungeonRes currentDungeon;
    [Export] private PackedScene[] allRoom;
    public List<Room> rooms { get; private set; }
    private RandomNumberGenerator rand = new RandomNumberGenerator();

    private const float MARGIN_SIZE = 1280f;



    public void SpawnDungeon()
    {
        rooms = new List<Room>();
        for (int i = 0; i < currentDungeon.DungeonPath.Count; i++)
        {
            Vector2I lRoomPos = currentDungeon.DungeonPath[i];
            GD.Print(lRoomPos + " " + currentDungeon.DungeonData[lRoomPos].doorConnected.Count);

            Room lRoom = (Room)allRoom[rand.RandiRange(0, allRoom.Length - 1)].Instantiate();
            AddChild(lRoom);
            rooms.Add(lRoom);
            if (i == 0 || i == currentDungeon.DungeonPath.Count - 1) lRoom.Initiliaze(this, currentDungeon.DungeonData[lRoomPos].doorConnected[0], currentDungeon.DungeonData[lRoomPos].doorConnected[0]);
            else lRoom.Initiliaze(this, currentDungeon.DungeonData[lRoomPos].doorConnected[0], currentDungeon.DungeonData[lRoomPos].doorConnected[1]);

            lRoom.GlobalPosition = (Vector2)lRoomPos * MARGIN_SIZE;

        }

    }

}
