using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//run in editor
[ExecuteInEditMode]
public class LevelLength : MonoBehaviour
{
    public Transform[] waypoints;
    public float levelLength = 0f;
    // Update is called once per frame
    void Update()
    {
        levelLength = CalculateLevelLength();
    }

    private float CalculateLevelLength(){
        float length = 0f;
        if(waypoints.Length == 0){
            return 0f;
        }
        for(int i = 0; i < waypoints.Length - 1; i++){
            length += Vector3.Distance(waypoints[i].position, waypoints[i + 1].position);
        }
        return length;
    }
}
