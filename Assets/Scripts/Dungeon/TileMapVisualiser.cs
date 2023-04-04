using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* Code from SunnyValleyStudios 
    https://www.youtube.com/watch?v=W6cBwk0bRWE&t=58s

    Implemented by Raziel Maron
*/

public class TileMapVisualiser : MonoBehaviour
{
    
    [SerializeField]
    private Tilemap floorTileMap, wallTileMap;
    [SerializeField]
    private TileBase floorTile, wallTile;
    
    public void paintFloorTiles(IEnumerable<Vector3Int> floorPos){
        PaintTiles(floorPos, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector3Int> position, Tilemap tileMap, TileBase tile){
        foreach (var pos in position){
            PaintSingleTile(tileMap, tile, pos);

        }
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector3Int position){
        //var tilePosition = tileMap.WorldToCell(position);
        tileMap.SetTile(position, tile);
    }

    public void Clear(){
        floorTileMap.ClearAllTiles();
        if (wallTileMap != null){
            wallTileMap.ClearAllTiles();
        }
    }

    public void PaintSingleBasicWall(Vector3Int position){
        PaintSingleTile(wallTileMap, wallTile, position);
    }

}
