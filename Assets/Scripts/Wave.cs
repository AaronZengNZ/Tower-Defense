using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 1f;
    public int numberOfEnemies = 5;
    public float enemyHp = 0f;
    public float enemySpeed = 0f;
    void Awake(){
        if(enemyHp == 0){
            enemyHp = enemy.GetComponent<Enemy>().hp;
        }
        if(enemySpeed == 0){
            enemySpeed = enemy.GetComponent<Enemy>().moveSpeed;
        }
    }
}
