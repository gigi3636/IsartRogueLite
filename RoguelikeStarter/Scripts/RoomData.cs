using Godot;
using System;
using System.Collections.Generic;

public partial class RoomData 
{
    public enum RoomType
    {
        EMPTY = 0,
        FightRoom = 1,
        LootRoom = 2,
        BossRoom = 3,
        SecretRoom = 4
    }

    public RoomType currentRoomType;
    public List<int> doorConnected { get; private set; } = new List<int> ();

    public void Initialize(RoomType pRoomType)
    {
        currentRoomType = pRoomType;
    }

    public void AddConnectedDoor(int pDoorPosition, bool pIsLastRoomDoor)
    {
        int lNewDoor = pDoorPosition;

        if (pIsLastRoomDoor)
        {
            lNewDoor = (pDoorPosition + 2) % 4;
        }
        
        if (doorConnected.Contains(lNewDoor))
        {
            GD.Print(" problème");
            GD.Print("door position "+pDoorPosition);
            return;
        }



        doorConnected.Add(lNewDoor);

    }

}
