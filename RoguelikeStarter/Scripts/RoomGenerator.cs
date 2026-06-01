using Godot;
using System;
using System.Collections.Generic;

public partial class RoomGenerator : Node
{
    private const int DOOR_POSSIBLE_AMOUNT = 4;

    private HashSet<Vector2I> rooms;
    private RandomNumberGenerator rand = new RandomNumberGenerator();
    private Vector2I previousRoom;


    public override void _Ready()
    {
        GenerateDungeon(100);
    }

    public void GenerateDungeon(int pRoomNumberToSpawn)
    {

        int lRoomCounter = 0;

        int lTryCounter = 0;

        // Keep trying to spawn all the room of the dungeon , restart if contatc one issue
        while (lRoomCounter <= pRoomNumberToSpawn)
        {
            lTryCounter ++;
            rooms = new HashSet<Vector2I>();
            rooms.Add(Vector2I.Zero);

            previousRoom = Vector2I.Zero;
            lRoomCounter = 1;

            for (int i = 0; i <= pRoomNumberToSpawn; i++)
            {
                Vector2I lNewRoomCo;

                for (int j = 0; j < DOOR_POSSIBLE_AMOUNT; j++)
                {
                    lNewRoomCo = previousRoom + CheckDirection(rand.RandiRange(1,4));
                    if (!rooms.Contains(lNewRoomCo))
                    {
                        rooms.Add(lNewRoomCo);
                        previousRoom =lNewRoomCo;

                        //GD.Print(" new room added " + lNewRoomCo);
                        lRoomCounter ++;
                        GD.Print(lRoomCounter);
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
        else if (pIndex == 2) return Vector2I.Right;
        else if (pIndex == 3) return Vector2I.Down;
        return Vector2I.Left;
    }
}

