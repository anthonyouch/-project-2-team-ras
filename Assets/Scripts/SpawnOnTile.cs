using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class SpawnOnTile
{
    
    public static void SetRandomPositionBounds(GameObject obj, Tilemap tilemap, BoundsInt bounds){
        List<Vector3Int> tilePositions = getAllAllocTilesBound(tilemap, bounds);

        obj.transform.SetPositionAndRotation(tilePositions[Random.Range(0, tilePositions.Count)], Quaternion.identity);
    }


    public static void SetRandomPosition(GameObject obj, Tilemap tilemap){
        List<Vector3Int> tilePositions = getAllAllocTiles(tilemap);

        obj.transform.SetPositionAndRotation(tilePositions[Random.Range(0, tilePositions.Count)], Quaternion.identity);
    }


    public static List<Vector3Int> getAllAllocTiles(Tilemap t){
        List<Vector3Int> output = new List<Vector3Int>();

        t.CompressBounds();
        var bounds = t.cellBounds;

        for (int i=bounds.min.x; i<bounds.max.x; i++){
            for (int j=bounds.min.z; j<bounds.max.z; j++){
                Vector3Int cellPos = new Vector3Int(i, 0, j);
                if (t.GetTile(cellPos) != null){
                    output.Add(cellPos);
                }
            }
        }
        return output;
    }

    public static List<Vector3Int> getAllAllocTilesBound(Tilemap t, BoundsInt bounds){
        List<Vector3Int> output = new List<Vector3Int>();

        for (int i=bounds.min.x; i<bounds.max.x; i++){
            for (int j=bounds.min.z; j<bounds.max.z; j++){
                Vector3Int cellPos = new Vector3Int(i, 0, j);
                if (t.GetTile(cellPos) != null){
                    output.Add(cellPos);
                }
            }
        }
        return output;
    }

}
