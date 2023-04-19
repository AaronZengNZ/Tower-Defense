using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;
    public Transform[] waypoints2;
    public float enemyHp = 3f;
    public float enemySpeed = 2f;
    public float spawnSpeed = 1f;   
    public Wave[] waves;
    public float[] pathNum;
    public float waveNum = 1f;
    float enemiesLeft = 0f;
    public UpgradeScript upgrades;
    bool upgrading = false;
    public float[] upgradeRarities;
    public SceneManager sceneManager;
    public TextMeshProUGUI waveText;
    public float extraEnemies = 1f;
    public float fasterSpawn = 1f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateValuesToWave();
        StartCoroutine(SpawnEnemies());
        StartCoroutine(WaveTextFade());
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
            StartCoroutine(WaveTextFade());
        }
    }

    IEnumerator WaveTextFade(){
        waveText.text = "Wave " + waveNum;
        waveText.color = new Color(1, 1, 1, 0);
        while(waveText.color.a < 1){
            waveText.color = new Color(1, 1, 1, waveText.color.a + 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        while(waveText.color.a > 0){
            waveText.color = new Color(1, 1, 1, waveText.color.a - 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void UpdateValuesToWave(){
        enemyPrefab = waves[(int)waveNum - 1].enemy;
        spawnSpeed = waves[(int)waveNum - 1].spawnRate / fasterSpawn;
        enemiesLeft = UnityEngine.Mathf.Floor(waves[(int)waveNum - 1].numberOfEnemies * extraEnemies);
        enemyHp = waves[(int)waveNum - 1].enemyHp;
        enemySpeed = waves[(int)waveNum - 1].enemySpeed;
    }

    public void SpawnEnemy(){
        //if pathnum is 10, spawn it on both lanes
        if(pathNum[(int)waveNum - 1] == 10){
            GameObject enemy = Instantiate(enemyPrefab, waypoints[0].position, Quaternion.identity);
            enemy.GetComponent<Enemy>().waypoints = waypoints;
            enemy.GetComponent<Enemy>().hp = enemyHp;
            enemy.GetComponent<Enemy>().moveSpeed = enemySpeed;
            GameObject enemy2 = Instantiate(enemyPrefab, waypoints2[0].position, Quaternion.identity);
            enemy2.GetComponent<Enemy>().waypoints = waypoints2;
            enemy2.GetComponent<Enemy>().hp = enemyHp;
            enemy2.GetComponent<Enemy>().moveSpeed = enemySpeed;
        }
        else if(pathNum[(int)waveNum - 1] == 2){
            GameObject enemy = Instantiate(enemyPrefab, waypoints2[0].position, Quaternion.identity);
            enemy.GetComponent<Enemy>().waypoints = waypoints2;
            enemy.GetComponent<Enemy>().hp = enemyHp;
            enemy.GetComponent<Enemy>().moveSpeed = enemySpeed;
        }
        else{
            GameObject enemy = Instantiate(enemyPrefab, waypoints[0].position, Quaternion.identity);
            enemy.GetComponent<Enemy>().waypoints = waypoints;
            enemy.GetComponent<Enemy>().hp = enemyHp;
            enemy.GetComponent<Enemy>().moveSpeed = enemySpeed;
        }
    }
}
