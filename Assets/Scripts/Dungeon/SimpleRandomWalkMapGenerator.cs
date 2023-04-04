using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using System.Diagnostics;

/*
Code from https://www.youtube.com/c/SunnyValleyStudio

Implemented by Raziel Maron, including original methods
*/

public class SimpleRandomWalkMapGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    public SimpleRandomWalkData WalkParameters;

    [SerializeField]
    protected bool addWalls = false;

    protected override void RunProceduralGeneration(){
        HashSet<Vector3Int> floorPositions = RunRandomWalk(startPosition, WalkParameters);
        tilemapVisualiser.Clear();
        tilemapVisualiser.paintFloorTiles(floorPositions);
        if (addWalls){
            WallGenerator.createWalls(floorPositions, tilemapVisualiser);
        }
    }



    protected HashSet<Vector3Int> RunRandomWalk(Vector3Int startPosition, SimpleRandomWalkData WalkParameters){
        var currentPos = startPosition;
        HashSet<Vector3Int> output = new HashSet<Vector3Int>();

        for (int i=0; i<WalkParameters.iterations; i++){
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos, WalkParameters.walkLen);
            output.UnionWith(path);
            if (WalkParameters.StartRandomlyEachIteration){
                currentPos = output.ElementAt(Random.Range(0, output.Count));
            }
        }

        return output;
    }

    protected HashSet<Vector3Int> RunBoundedRandomWalk(Vector3Int startPosition, SimpleRandomWalkData WalkParameters, BoundsInt bounds){
        var currentPos = startPosition;
        HashSet<Vector3Int> output = new HashSet<Vector3Int>();

        for (int i=0; i<WalkParameters.iterations; i++){
            var path = ProceduralGenerationAlgorithms.BoundRandomWalk(currentPos, WalkParameters.walkLen, bounds);
            if (path == null){continue;}
            
            output.UnionWith(path);
            if (WalkParameters.StartRandomlyEachIteration){
                currentPos = output.ElementAt(Random.Range(0, output.Count));
            }
        }

        return output;
    }

}