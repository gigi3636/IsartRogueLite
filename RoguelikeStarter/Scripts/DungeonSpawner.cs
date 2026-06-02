using Godot;
using System;

public partial class DungeonSpawner : Node2D
{
    [Export] private CurrentDungeonRes currentDungeon;

    public void SpawnDungeon()
    {
        foreach (Vector2I lRoomPos in currentDungeon.DungeonPath)
        {
            GD.Print(lRoomPos + " " + currentDungeon.DungeonData[lRoomPos].doorConnected.Count);
        }
    }
}
