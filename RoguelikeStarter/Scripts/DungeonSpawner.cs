using Godot;
using System;

public partial class DungeonSpawner : Node2D
{
    [Export] private CurrentDungeonRes currentDungeon;
    [Export] private PackedScene[] allRoom;
    private RandomNumberGenerator rand = new RandomNumberGenerator();

    private const float MARGIN_SIZE = 1280f;



    public void SpawnDungeon()
    {
        foreach (Vector2I lRoomPos in currentDungeon.DungeonPath)
        {
            GD.Print(lRoomPos + " " + currentDungeon.DungeonData[lRoomPos].doorConnected.Count);

            Room lRoom = (Room)allRoom[rand.RandiRange(0, allRoom.Length-1)].Instantiate();
            AddChild(lRoom);


            lRoom.GlobalPosition = (Vector2)lRoomPos * MARGIN_SIZE;



        }
    }

}
