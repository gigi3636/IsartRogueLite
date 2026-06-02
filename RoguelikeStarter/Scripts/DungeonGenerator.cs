using Godot;
using System.Collections.Generic;

public partial class DungeonGenerator : Node
{
    [Export] private CurrentDungeonRes currentDungeon;

    private const int DOOR_POSSIBLE_AMOUNT = 4;

    private RandomNumberGenerator rand = new RandomNumberGenerator();
    private Vector2I previousRoom;

    public override void _Ready()
    {
    }

    public void GenerateDungeon(int pRoomNumberToSpawn)
    {
        int lRoomCounter = 0;
        int lTryCounter = 0;

        while (lRoomCounter <= pRoomNumberToSpawn)
        {
            lTryCounter++;

            currentDungeon.DungeonRoomsPosRooms = new HashSet<Vector2I>();
            currentDungeon.DungeonPath = new List<Vector2I>();

            currentDungeon.DungeonRoomsPosRooms.Add(Vector2I.Zero);
            currentDungeon.DungeonPath.Add(Vector2I.Zero);

            previousRoom = Vector2I.Zero;
            lRoomCounter = 1;

            for (int i = 0; i <= pRoomNumberToSpawn; i++)
            {
                Vector2I lNewRoomCo;

                List<int> lRoomPossible = new List<int>();

                for (int j = 1; j < DOOR_POSSIBLE_AMOUNT+1; j++)
                {
                    lRoomPossible.Add(j);
                }

                while (lRoomPossible.Count > 0)
                {
                    int lRoomIndexToTry = rand.RandiRange(0, lRoomPossible.Count-1);
                    GD.Print(lRoomIndexToTry);

                    lNewRoomCo = previousRoom + CheckDirection(lRoomPossible[lRoomIndexToTry]);
                    lRoomPossible.RemoveAt(lRoomIndexToTry);


                    if (!currentDungeon.DungeonRoomsPosRooms.Contains(lNewRoomCo))
                    {
                        currentDungeon.DungeonRoomsPosRooms.Add(lNewRoomCo);
                        currentDungeon.DungeonPath.Add(lNewRoomCo);
                        previousRoom = lNewRoomCo;

                        lRoomCounter++;
                        
                        if (lRoomCounter == pRoomNumberToSpawn) GD.Print("spawnRedRoom");

                        break;
                    }
                }
            }
        }
        GD.Print("Try to generate the dungeon " + lTryCounter + " time");
    }

    private Vector2I CheckDirection(int pIndex)
    {
        if (pIndex == 1) return Vector2I.Up;
        if (pIndex == 2) return Vector2I.Right;
        if (pIndex == 3) return Vector2I.Down;
        return Vector2I.Left;
    }
}