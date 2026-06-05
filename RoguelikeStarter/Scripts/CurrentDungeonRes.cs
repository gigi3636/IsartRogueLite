using Godot;
using System;
using System.Collections.Generic;

public partial class CurrentDungeonRes : Resource
{
    public Dictionary<Vector2I, RoomData> DungeonData;
    public List<Vector2I> DungeonPath;
    public List<RoomData> DungeonRooms;


}
