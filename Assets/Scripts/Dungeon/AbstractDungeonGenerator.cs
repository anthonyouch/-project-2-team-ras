using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Code from https://www.youtube.com/c/SunnyValleyStudio

Implemented by Raziel Maron
*/


public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TileMapVisualiser tilemapVisualiser = null;
    [SerializeField]
    protected Vector3Int startPosition = Vector3Int.zero;

    public void GenerateDungeon(){
        ClearTilemap();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
    
    public void ClearTilemap(){
        tilemapVisualiser.Clear();
    }
}
