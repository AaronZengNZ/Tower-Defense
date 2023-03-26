using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject projectile;
    public float shootSpeed = 4f;
    public float range = 10f;
    public float damage = 1f;
    public float projectileSpeed = 10f;
    public GameObject sprite;
    public Transform firePoint;
    public bool isRight = false;
    public Rigidbody2D rb;
    public GameObject rangeIndicator;
    public float updateSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootProjectiles());
        StartCoroutine(AutomateManualUpdate());
    }

    IEnumerator ShootProjectiles(){
        while(true){
            yield return new WaitForSeconds(1 / shootSpeed);
            ShootProjectile();
        }
    }

    public void ShootProjectile(){
        if(GetClosestEnemy(this.transform)){
            GameObject proj = Instantiate(projectile, firePoint.position, Quaternion.identity);
            proj.GetComponent<Projectile>().target = GetClosestEnemy(this.transform);
            proj.GetComponent<Projectile>().damage = damage;
            proj.GetComponent<Projectile>().speed = projectileSpeed;
            proj.GetComponent<Projectile>().playerScript = this;
        }
    }

    public Transform GetClosestEnemy(Transform startPoint){
        Transform targetEnemy = null;
        float furthestDistance = 0;
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
            float distance = Vector2.Distance(startPoint.position, enemy.transform.position);
            if(distance <= range){
                //compare enemy's enemy script's distnace to furthest distance
                if(enemy.GetComponent<Enemy>().distance > furthestDistance){
                    furthestDistance = enemy.GetComponent<Enemy>().distance;
                    targetEnemy = enemy.transform;
                }
            }
        }
        if(targetEnemy == null){
            return null;
        }
        return targetEnemy;
    }

    IEnumerator AutomateManualUpdate(){
        while(true){
            yield return new WaitForSeconds(1f / updateSpeed);
            ManualUpdate();
        }
    }

    void ManualUpdate()
    {
        //update range indicator
        rangeIndicator.transform.localScale = new Vector3(range * 1.9f, range * 1.9f, 1);
        //move player
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        //add force
        rb.velocity = rb.velocity / 1.5f;
        rb.AddForce(direction * moveSpeed * updateSpeed);
        //cap velocity
        if(rb.velocity.magnitude > moveSpeed){
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        //shoot projectiles
        if(Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(ShootProjectiles());
        }   
        //flip sprite based on x input
        if(x > 0){
            isRight = true;
            sprite.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(x < 0){
            isRight = false;
            sprite.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
