using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Tilemaps.Tilemap tilemap;

    
    
    //public Wave[] waves;
    public EnemyController enemy;
    //Wave currentWave;
    

    int enemiesRemainingToSpawn;
    static int numberOfEnemiesThisRound = 5;
    
    public float timeBetweenSpawns; 
    float nextSpawnTime;

    void Start() {
        //NextWave();
        enemiesRemainingToSpawn = numberOfEnemiesThisRound;
        SpawnOnTile.SetRandomPosition(enemy.gameObject, tilemap);
    }


    void Update() {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + timeBetweenSpawns;

            // Can spawn near to player using getAllAllocTilesBound
            // Can also spawn in groups near other enemies
            List<Vector3Int> tilePositions = SpawnOnTile.getAllAllocTiles(tilemap);
            EnemyController spawnedEnemy = Instantiate(enemy, tilePositions[Random.Range(0, tilePositions.Count)], Quaternion.identity) as EnemyController;
            
        }
    }

    // void NextWave() {
    //     currentWaveNumber++;
    //     currentWave = waves[currentWaveNumber -1];
    //     enemiesRemainingToSpawn = currentWave.enemyCount;
    // }


    // [System.Serializable]
    // public class Wave {
    //     public int enemyCount;
    //     public float timeBetweenSpawns;
    // } 
    
    public void doubleEnemies() {
        numberOfEnemiesThisRound *= 2;
    }
    
    
    
}
