using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Code from https://www.youtube.com/c/SunnyValleyStudio

Implemented by Raziel Maron
*/


public static class WallGenerator
{
    public static void createWalls(HashSet<Vector3Int> floorPositions, TileMapVisualiser tilemapVisualiser){

        var basicWallPositions = FindWallsInDirections(floorPositions, Directions3D.allDirnList);
        foreach(var pos in basicWallPositions){
            tilemapVisualiser.PaintSingleBasicWall(pos);
        }
    }

    private static HashSet<Vector3Int> FindWallsInDirections(HashSet<Vector3Int> floorPositions, List<Vector3Int> DirnList){
        HashSet<Vector3Int> wallPositions = new HashSet<Vector3Int>();
        foreach(var pos in floorPositions){
            foreach(var dirn in DirnList){
                var neighbourPos = pos + dirn;
                if (floorPositions.Contains(neighbourPos) ==  false){
                    wallPositions.Add(neighbourPos);
                }
            }
        }
        return wallPositions;

    }
}
