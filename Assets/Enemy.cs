using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float currentWaypoint = 0f;
    public float hp = 3f;
    public float distance = 0f;
    public GameObject sprite;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[0].transform.position;
    }

    // go through waypoints
    void Update()
    {
        Move();
    }
    void FixedUpdate()
    {
        distance++;
    }

    public void Move(){
        if(currentWaypoint < waypoints.Length){
            transform.position = Vector2.MoveTowards(transform.position, waypoints[(int)currentWaypoint].transform.position, moveSpeed * Time.deltaTime);
            //if distance to waypoint is less than 0.1f, go to next waypoint
            if(Vector2.Distance(transform.position, waypoints[(int)currentWaypoint].transform.position) < 0.1f){
                currentWaypoint += 1f;
            }
            //flip sprite according to waypoint direction
            if(waypoints[(int)currentWaypoint].transform.position.x > transform.position.x){
                sprite.transform.localScale = new Vector3(1, 1, 1);
            }
            else if(waypoints[(int)currentWaypoint].transform.position.x < transform.position.x){
                sprite.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else{
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage){
        UnityEngine.Debug.Log("hit");
        hp -= damage;
        if(hp <= 0){
            Destroy(gameObject);
        }
    }
}