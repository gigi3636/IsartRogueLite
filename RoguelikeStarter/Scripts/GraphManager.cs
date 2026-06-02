using Godot;
using System;

public partial class GraphManager : Node
{
    [Export] private DungeonGenerator dungeonGeneratorRef;
    [Export] private GraphVisualiser graphVisualiserRef;
    [Export] private DungeonSpawner dungeonSpawnerRef;
    [Export] private int roomNumber = 1;

    public override void _Ready()
    {
        dungeonGeneratorRef.GenerateDungeon(100);

        graphVisualiserRef.GenerateGraph();

        dungeonSpawnerRef.SpawnDungeon();
    }
}
