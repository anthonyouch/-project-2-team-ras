using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif
 
public class PrefabTile : UnityEngine.Tilemaps.TileBase
{
    public GameObject TileAssociatedPrefab;
 
    
    public float prefabXOffset = 0f;
    public float prefabYOffset = 0f;
    public float prefabZOffset = 0f;
 
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
 
        //This prevents rogue prefab objects from appearing when the Tile palette is present
#if UNITY_EDITOR
        if (go != null)
        {
            if (go.scene.name == null)
            {
                DestroyImmediate(go);
            }
        }
#endif
 
        if (go != null)
        {
            //Modify position of GO to match middle of Tile sprite
            go.transform.position = new Vector3(position.x + prefabXOffset
                , position.y + prefabYOffset
                , position.z + prefabZOffset);
 
        }
 
        return true;
    }
 
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Prefab Tile")]
    public static void CreatePrefabTiles()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Prefab Tile", "New Prefab Tile", "asset", "Save Prefab Tile", "Assets");
 
        if (path == "")
            return;
 
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<PrefabTile>(), path);
    }
#endif
 
 
public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        if (TileAssociatedPrefab && tileData.gameObject==null){
            tileData.gameObject = TileAssociatedPrefab;
        }

    }
}