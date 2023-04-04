using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
Code from https://www.youtube.com/c/SunnyValleyStudio

Implemented by Raziel Maron
*/

public class RoomFirstDungeonGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private RoomsFirstDungeonData RoomParameters;

    
    void Start(){
        GenerateDungeon();
    }


    protected override void RunProceduralGeneration(){
        CreateRooms();
    }

    private void CreateRooms(){
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt(new Vector3Int(startPosition.x, startPosition.y-1, startPosition.z), new Vector3Int(RoomParameters.dungeonWidth, startPosition.y+1, RoomParameters.dungeonHeight)), 
                RoomParameters.minRoomWidth, RoomParameters.minRoomHeight, RoomParameters.maxSizeScalarX, RoomParameters.maxSizeScalarZ);
        
        HashSet<Vector3Int> floor = new HashSet<Vector3Int>();
        
        if (RoomParameters.randomWalkRooms){
            floor = CreateRandomWalkRooms(roomsList);
        } else {
            floor = CreateSimpleRooms(roomsList);
        }

        List<Vector3Int> roomCenter = new List<Vector3Int>();
        foreach(var room in roomsList){
            
            roomCenter.Add(getCenter(room));
        }
        
        HashSet<Vector3Int> corridors = ConnectRooms(roomCenter);

        floor.UnionWith(corridors);


        tilemapVisualiser.paintFloorTiles(floor);
        if (addWalls){
            WallGenerator.createWalls(floor, tilemapVisualiser);
        }
    }



    private HashSet<Vector3Int> CreateSimpleRooms(List<BoundsInt> roomsList){
        HashSet<Vector3Int> floor = new HashSet<Vector3Int>();
        foreach(var room in roomsList){
            for(int col=RoomParameters.offset; col < room.size.x-RoomParameters.offset; col++){
                for(int row=RoomParameters.offset; row<room.size.z-RoomParameters.offset; row++){
                    Vector3Int position = (Vector3Int)room.min + new Vector3Int(col, 0, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
 //
    private HashSet<Vector3Int> ConnectRooms(List<Vector3Int> roomCenters){
        HashSet<Vector3Int> corridors = new HashSet<Vector3Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count>0){
            Vector3Int closestPoint = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closestPoint);
            HashSet<Vector3Int> newCorridor = CreateCorridor(currentRoomCenter, closestPoint);
            currentRoomCenter = closestPoint;
            corridors.UnionWith(newCorridor);

        }

        return corridors;
    }

    private Vector3Int FindClosestPointTo(Vector3Int currentRoomCenter, List<Vector3Int> roomCenters){
        Vector3Int closest = Vector3Int.zero;
        float length = float.MaxValue;

        foreach(var pos in roomCenters){
            float currentLen = Vector3.Distance(pos, currentRoomCenter);
            if (currentLen < length){
                length = currentLen;
                closest = pos;
            }
        }
        return closest;
    }

    private HashSet<Vector3Int> CreateCorridor(Vector3Int currentRoomCenter, Vector3Int closestPoint){
        HashSet<Vector3Int> corridor = new HashSet<Vector3Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        var prevPos = position;

        while (position.z != closestPoint.z){
            prevPos = position;
            if (position.z < closestPoint.z){
                position += Vector3Int.forward;
            } else if (position.z > closestPoint.z){
                position += Vector3Int.back;
            }
            if (position.z%2==0){
                corridor.Add(position);
            }
        }
        while (position.x != closestPoint.x){
            prevPos = position;
            if (position.x < closestPoint.x){
                position += Vector3Int.right;
            } else if (position.x > closestPoint.x){
                position += Vector3Int.left;
            }
            if (position.x%2==0){
                corridor.Add(position);
            }
        }
        return corridor;
    }


    private HashSet<Vector3Int> CreateRandomWalkRooms(List<BoundsInt> roomList){
        HashSet<Vector3Int> floor = new HashSet<Vector3Int>();
        foreach(var room in roomList){

            var roomCenter = getCenter(room);
            var bounds = room;
            

            bounds.xMin = bounds.xMin + RoomParameters.offset;
            bounds.xMax = bounds.xMax - RoomParameters.offset;
            bounds.zMin = bounds.zMin + RoomParameters.offset;
            bounds.zMax = bounds.zMax - RoomParameters.offset;
            

            var roomFloor = RunBoundedRandomWalk(roomCenter, WalkParameters, bounds);

            floor.UnionWith(roomFloor);
        }
        return floor;
    }

    private Vector3Int getCenter(BoundsInt b){
        Vector3Int center = Vector3Int.RoundToInt(b.center);

        if (center.x % 2 != 0){

            center.x++;

        }
        if (center.y %2 != 0){
            
            center.y++;

        }
        if (center.z%2 !=0){
            center.z++;
        }
            
        return center;
    }
}

