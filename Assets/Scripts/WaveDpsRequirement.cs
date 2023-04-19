using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//run in editor
[ExecuteInEditMode]
public class WaveDpsRequirement : MonoBehaviour
{
    public float enemies = 0f;
    public float enemyHp = 0f;
    public float enemySpawntime = 1f;
    public float enemySpeed = 1f;
    public float pathLength = 1f;
    public float enemyGroupSize = 1f;
    public float dpsRequirement = 0f;

    void Update()
    {
        dpsRequirement = CalculateDpsRequirement();
    }

    private float CalculateDpsRequirement(){
        //now multiply timetoKillBeforeNext by the number of enemies
        float softtimeForEnemy = pathLength / enemies / enemySpeed;
        float requirement = enemyHp * enemyGroupSize / (enemySpawntime + softtimeForEnemy);
        return requirement;
    }
}
