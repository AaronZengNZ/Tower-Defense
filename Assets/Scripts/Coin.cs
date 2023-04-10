using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    float value = 1f;
    //find player
    public GameObject player;
    public Rigidbody2D rb;
    public float speed = 1f;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //multiply velocity by 0.8f
        rb.velocity *= 0.9f;
        rb.AddForce((player.transform.position - transform.position).normalized * speed);
    }

    //on collision with player, delete slef
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            GameObject.Find("Money Manager").GetComponent<MoneyScript>().moneys += value;
            Destroy(gameObject);
        }
    }
}
