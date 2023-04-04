using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
Code from https://www.youtube.com/c/SunnyValleyStudio

Implemented by Raziel Maron, Including original methods:
    BoundRandomWalk
    ContainsMinMax
*/


public static class ProceduralGenerationAlgorithms
{
    
    public static HashSet<Vector3Int> SimpleRandomWalk(Vector3Int startPos, int walkLen){
        HashSet<Vector3Int> path = new HashSet<Vector3Int>();

        path.Add(startPos);
        Vector3Int prevPos = startPos;

        for (int i=0; i< walkLen; i++){
            var newPosition = prevPos + Directions3D.getRandomCardinalDirn();
            path.Add(newPosition);
            prevPos = newPosition;
        }
        return path;
    }


    public static HashSet<Vector3Int> BoundRandomWalk(Vector3Int startPos, int walkLen, BoundsInt bounds){
        HashSet<Vector3Int> path = new HashSet<Vector3Int>();
        
        if (!ContainsMinMax(bounds, startPos)){
            return path;
        }

        path.Add(startPos);
        Vector3Int prevPos = startPos;

        for (int i=0; i< walkLen; i++){
            var newPosition = prevPos + Directions3D.getRandomCardinalDirn();
            if (!ContainsMinMax(bounds, newPosition)){ 
                return path;
            }
            path.Add(newPosition);
            prevPos = newPosition;
        }
        return path;
    }
    


public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight, int maxSizeScalarX, int maxSizeScalarZ){

    Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
    List<BoundsInt> roomsList = new List<BoundsInt>();

    roomsQueue.Enqueue(spaceToSplit);

    while (roomsQueue.Count>0){
        var room = roomsQueue.Dequeue();
        if (room.size.z>=minHeight && room.size.x>=minWidth){
            if (Random.value < 0.5f){
                if (room.size.x >= minWidth*maxSizeScalarX) {
                    SplitVertically(minWidth, roomsQueue, room);
                } else if (room.size.z >= minHeight*maxSizeScalarZ){
                    SplitHorizontally(minHeight, roomsQueue, room);
                } else {
                    roomsList.Add(room);
                }
            }   else {
                if (room.size.z >= minHeight*maxSizeScalarZ) {
                    SplitHorizontally(minHeight, roomsQueue, room);
                } else if (room.size.x >= minWidth*maxSizeScalarX){
                    SplitVertically(minWidth, roomsQueue, room);
                } else {
                    roomsList.Add(room);
                }
            } 
        }
    }
    return roomsList;
}


private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room){
    var zSplit = Random.Range(1, room.size.z);
    BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, room.size.y, zSplit));
    BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y, room.min.z + zSplit), 
                    new Vector3Int(room.size.x, room.size.y, room.size.z - zSplit));
    roomsQueue.Enqueue(room1);
    roomsQueue.Enqueue(room2);
}

private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room){
    var xSplit = Random.Range(1, room.size.x);
    BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
    BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), 
                    new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
    roomsQueue.Enqueue(room1);
    roomsQueue.Enqueue(room2);
}


public static bool ContainsMinMax(BoundsInt b, Vector3Int pos){
    if (pos.x >= b.xMin && pos.x <= b.xMax 
        && pos.z >= b.zMin && pos.z <= b.zMax){
        return true;
    }
    return false;
}


}


