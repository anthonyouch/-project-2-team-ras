using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StairFunction : MonoBehaviour
{

    Player player;
    
    public Text newLevelTitle;

    public static int currentLevel = START_LEVEL_INDEX;
    public static readonly int MaxLevels = 3;
    public static readonly int START_LEVEL_INDEX = 0;

    [SerializeField]
    public UnityEngine.Tilemaps.Tilemap tilemap;

    Spawner spawner;


    void Start(){

        OnNextLevel(currentLevel);
        
        spawner = FindObjectOfType<Spawner>();
        SpawnOnTile.SetRandomPosition(gameObject, tilemap);
        player = FindObjectOfType<Player>();
        //transform.Translate(0f, -.1f, 0.2f, Space.World);
    }
    
    public void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            //Debug.Log("End level");
            // End level
            
            if (currentLevel < MaxLevels){
                SceneManager.LoadScene("SampleScene");
                spawner.doubleEnemies();
                currentLevel += 1;
            } else {
                SceneManager.LoadScene("EndScene");
            }
            
            //OnNextLevel(currentLevel);
            
        }
    }

    void OnNextLevel(int levelNumber) { 
        newLevelTitle.text = "Level " + (MaxLevels-levelNumber);
    }
}
