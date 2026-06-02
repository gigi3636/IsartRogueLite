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

            // Reset all the resource data
            currentDungeon.DungeonData = new Dictionary<Vector2I, RoomData>();
            currentDungeon.DungeonPath = new List<Vector2I>();
            currentDungeon.DungeonRooms = new List<RoomData>();

            // Create the first room
            RoomData lFirstRoom = new RoomData();
            lFirstRoom.Initialize(RoomData.RoomType.EMPTY);


            currentDungeon.DungeonData.Add(Vector2I.Zero, lFirstRoom);
            currentDungeon.DungeonPath.Add(Vector2I.Zero);
            currentDungeon.DungeonRooms.Add(lFirstRoom);

            previousRoom = Vector2I.Zero;

            lRoomCounter = 1;

            for (int i = 0; i <= pRoomNumberToSpawn; i++)
            {
                Vector2I lNewRoomCo;

                List<int> lRoomPossible = new List<int>();

                for (int j = 0; j < DOOR_POSSIBLE_AMOUNT ; j++)
                {
                    lRoomPossible.Add(j);
                }

                while (lRoomPossible.Count > 0)
                {
                    int lRoomIndexToTry = rand.RandiRange(0, lRoomPossible.Count - 1);

                    lNewRoomCo = previousRoom + CheckDirection(lRoomPossible[lRoomIndexToTry]);


                    if (!currentDungeon.DungeonData.ContainsKey(lNewRoomCo))
                    {
                        // Add the first door for the first room
                        if (currentDungeon.DungeonPath.Count == 1) currentDungeon.DungeonData[previousRoom].AddConnectedDoor(lRoomPossible[lRoomIndexToTry], false);

                        // Spawn new room data
                        RoomData lNewRoomData = new RoomData();
                        lNewRoomData.Initialize(RoomData.RoomType.EMPTY);

                        // Add the entry door from the last room and the next door 
                        lNewRoomData.AddConnectedDoor(currentDungeon.DungeonData[previousRoom].doorConnected[^1], true);
                        lNewRoomData.AddConnectedDoor(lRoomPossible[lRoomIndexToTry], false);

                        currentDungeon.DungeonData.Add(lNewRoomCo, lNewRoomData);
                        currentDungeon.DungeonPath.Add(lNewRoomCo);

                        currentDungeon.DungeonRooms.Add(lNewRoomData);
                        previousRoom = lNewRoomCo;

                        lRoomCounter++;

                        if (lRoomCounter == pRoomNumberToSpawn)
                        {
                            GD.Print("spawnRedRoom");
                            GD.Print("Try to generate the dungeon " + lTryCounter + " time");
                            return;
                        }

                        break;
                    }
                    lRoomPossible.RemoveAt(lRoomIndexToTry);

                }
            }
        }
    }

    private Vector2I CheckDirection(int pIndex)
    {
        if (pIndex == 0) return Vector2I.Up;
        if (pIndex == 1) return Vector2I.Right;
        if (pIndex == 2) return Vector2I.Down;
        return Vector2I.Left;
    }
}