using Godot;
using System.Collections.Generic;

public partial class GraphVisualiser : Node2D
{
    [Export] private CurrentDungeonRes currentDungeon;

    private const float CIRCLE_SIZE = 10f;
    private const float MARGIN_SIZE = 50f;

    public void GenerateGraph()
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        // Draw all the connection between the rooms
        for (int lI = 0; lI < currentDungeon.DungeonPath.Count - 1; lI++)
        {
            Vector2 lCurrentRoomPos = new Vector2(currentDungeon.DungeonPath[lI].X, currentDungeon.DungeonPath[lI].Y) * MARGIN_SIZE;
            Vector2 lNextRoomPos = new Vector2(currentDungeon.DungeonPath[lI + 1].X, currentDungeon.DungeonPath[lI + 1].Y) * MARGIN_SIZE;

            DrawLine(lCurrentRoomPos, lNextRoomPos, Colors.White, 2f);
        }

        // Draw all the rooms
        foreach (Vector2I lPos in currentDungeon.DungeonPath)
        {
            Vector2 lCurrentScreenPos = new Vector2(lPos.X, lPos.Y) * MARGIN_SIZE;
            DrawCircle(lCurrentScreenPos, CIRCLE_SIZE, Colors.DodgerBlue);
        }
    }
}