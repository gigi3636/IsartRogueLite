using Godot;
using System;

public partial class GraphManager : Node
{
    [Export] private DungeonGenerator dungeonGeneratorRef;
    [Export] private GraphVisualiser graphVisualiserRef;

    public override void _Ready()
    {
        dungeonGeneratorRef.GenerateDungeon(100);

        graphVisualiserRef.GenerateGraph(); 
    }
}
