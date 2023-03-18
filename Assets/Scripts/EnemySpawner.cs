using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;
    public float enemyHp = 3f;
    public float enemySpeed = 2f;
    public float spawnSpeed = 1f;   
    public Wave[] waves;
    public float waveNum = 1f;
    float enemiesLeft = 0f;
    // Start is called before the first frame update
    void Start()
    {
        UpdateValuesToWave();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies(){
        while(true){
            yield return new WaitForSeconds(spawnSpeed);
            if(enemiesLeft >= 1){
                SpawnEnemy();
                enemiesLeft--;
            }
            else{
                spawnSpeed = 0.1f;
                if(GameObject.FindGameObjectsWithTag("Enemy").Length > 0){
                    continue;
                }
                if(waveNum < waves.Length){
                    waveNum++;
                    UpdateValuesToWave();
                }
                else{
                    Debug.Log("You Win!");
                    break;
                }
            }
        }
    }

    public void UpdateValuesToWave(){
        enemyPrefab = waves[(int)waveNum - 1].enemy;
        spawnSpeed = waves[(int)waveNum - 1].spawnRate;
        enemiesLeft = waves[(int)waveNum - 1].numberOfEnemies;
        enemyHp = waves[(int)waveNum - 1].enemyHp;
        enemySpeed = waves[(int)waveNum - 1].enemySpeed;
    }

    public void SpawnEnemy(){
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().waypoints = waypoints;
        enemy.GetComponent<Enemy>().hp = enemyHp;
        enemy.GetComponent<Enemy>().moveSpeed = enemySpeed;
        
    }
}
