using System.Threading;
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
    public UpgradeScript upgrades;
    bool upgrading = false;
    public float[] upgradeRarities;
    public SceneManager sceneManager;
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
                if(waveNum == waves.Length){
                    if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
                        sceneManager.Win();
                    }
                }
                if(GameObject.FindGameObjectsWithTag("Enemy").Length > 0){
                    continue;
                }
                else if(upgrading == false){
                    upgrading = true;
                    upgrades.InstantiateUpgrades(upgradeRarities[(int)waveNum - 1]);
                }
            }
        }
    }

    public void NextWave(){
        if(waveNum < waves.Length){
            waveNum++;
            UpdateValuesToWave();
            upgrading = false;
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
