using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomsFirstDungeonParameters_", menuName = "PCG/RoomsFirstDungeonData")]
public class RoomsFirstDungeonData : ScriptableObject
{
    public int minRoomWidth = 4, minRoomHeight = 4;
    public int dungeonWidth = 20, dungeonHeight = 20;

    [Range(0,10)]
    public int offset = 1;

    [Range(2,10)]
    public int  maxSizeScalarX = 2;
    [Range(2,10)]
    public int maxSizeScalarZ = 2;

    public bool randomWalkRooms=false;
}
