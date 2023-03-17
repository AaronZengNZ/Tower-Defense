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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies(){
        while(true){
            yield return new WaitForSeconds(spawnSpeed);
            SpawnEnemy();
        }
    }

    public void SpawnEnemy(){
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().waypoints = waypoints;
        enemy.GetComponent<Enemy>().hp = enemyHp;
        enemy.GetComponent<Enemy>().moveSpeed = enemySpeed;
        
    }
}
