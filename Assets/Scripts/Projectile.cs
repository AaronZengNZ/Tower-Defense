using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed = 25f;
    public float damage = 1f;
    public Player playerScript;
    public float turnSpeed = 5f;
    public Rigidbody2D rb;
    //get the sprite renderer
    public SpriteRenderer spriteRenderer;

    public bool homing = false;
    public float noTurning = 10f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ManualUpdateAutomator());
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(target != null){
            float direction;
            //random number from -10 to 10
            float random = UnityEngine.Random.Range(-10f, 10f);
            if(playerScript.isRight){
                direction = 120 + random;
            }
            else{
                direction = 60 + random;
            }
            Quaternion rotation = Quaternion.AngleAxis(direction, Vector3.forward);
            transform.rotation = rotation;

        }
    }
    void ManualUpdate()
    {
        if(noTurning >= 0){
            noTurning -= 1;
            rb.velocity = transform.right * speed;
            return;
        }
        //rb.velocity = rb.velocity / 1.2f;
        if(target == null){
            if(homing == true){
            target = playerScript.GetClosestEnemy(this.transform);
            }
            else{
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.04f);
                if(spriteRenderer.color.a <= 0){
                    Destroy(gameObject);
                }
            }
            return;
        }
        else{
            //slow turn towards the target
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed / 100);
            //move towards the target
            rb.velocity = transform.right * speed;
        }
    }

    IEnumerator ManualUpdateAutomator(){
        //update every 50ms
        while(true){
            yield return new WaitForSeconds(0.05f);
            ManualUpdate();
        }
    }

    // on collision with tag enemy, hit target
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy"){
            UnityEngine.Debug.Log("hit");
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
