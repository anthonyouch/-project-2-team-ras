using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Directions3D
{

    public static List<Vector3Int> allDirnList = new List<Vector3Int>{
        new Vector3Int(0,0,2), //UP
        new Vector3Int(2,0,0), //RIGHT
        new Vector3Int(-2,0,0), //LEFT
        new Vector3Int(0,0,-2), //DOWN
        new Vector3Int(2,0,2), //UP-RIGHT
        new Vector3Int(-2,0,2), //UP-LEFT
        new Vector3Int(-2,0,-2), //DOWN-LEFT
        new Vector3Int(2,0,-2) //DOWN-RIGHT

    };


    public static List<Vector3Int> cardinalDirnList = new List<Vector3Int>{
        new Vector3Int(0,0,2), //UP
        new Vector3Int(2,0,0), //RIGHT
        new Vector3Int(-2,0,0), //LEFT
        new Vector3Int(0,0,-2) //DOWN
    };

    public static Vector3Int getRandomCardinalDirn(){
        return cardinalDirnList[Random.Range(0, cardinalDirnList.Count)];
    }

    public static Vector3Int getRandomDirn(){
        return allDirnList[Random.Range(0, allDirnList.Count)];
    }

}
