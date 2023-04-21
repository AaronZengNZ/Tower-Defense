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
    public float freezeEffect = 0f;
    public float stun = 0f;
    public float burnDamage = 0f;
    public bool homing = false;
    public float multishot = 1f;
    public string playerType = "rocket";
    public float lazerRot = 0f;
    public float lazerRotSpeed = 360f;
    public float shockwaveProjectiles = 36f;
    public float damagePercent = 100f;
    public float fireratePercent = 100f;
    public float rangePercent = 100f;
    public float bulletSpeedPercent = 100f;
    public float processedRange = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootProjectiles());
        StartCoroutine(AutomateManualUpdate());
    }

    IEnumerator ShootProjectiles(){
        while(true){
            float processedShootSpeed = shootSpeed * fireratePercent / 100f;
            if(playerType == "shockwave"){
                yield return new WaitForSeconds(4 / processedShootSpeed);
            }
            yield return new WaitForSeconds(1 / processedShootSpeed);
            ShootProjectile();
        }
    }

    public void ShootProjectile(){
        float processedDamage = damage * damagePercent / 100f;
        float processedProjectileSpeed = projectileSpeed * bulletSpeedPercent / 100f;
        if(GetClosestEnemy(this.transform)){
            //shoot mutlishot projectiles
            for(int i = 0; i < multishot; i++){
                GameObject proj = null;
                if(playerType == "lazer"){
                    proj = Instantiate(projectile, firePoint.position, Quaternion.Euler(0, 0, lazerRot));
                    lazerRot += (1f / updateSpeed) * lazerRotSpeed;
                }
                else if (playerType == "shockwave"){
                    //make angle start with a random value from 0 to 360f / (shockwaveProjectiles * multishot)
                    float angle = UnityEngine.Random.Range(0f, 360f / (shockwaveProjectiles * multishot));
                    for(int f = 0; f < shockwaveProjectiles * (multishot / 2 + 0.5f); f++){
                        proj = Instantiate(projectile, firePoint.position, Quaternion.Euler(0, 0, angle));
                        proj.GetComponent<Projectile>().target = GetClosestEnemy(this.transform);
                        proj.GetComponent<Projectile>().damage = processedDamage;
                        proj.GetComponent<Projectile>().speed = processedProjectileSpeed;
                        proj.GetComponent<Projectile>().playerScript = this;
                        proj.GetComponent<Projectile>().freezeEffect = freezeEffect;
                        proj.GetComponent<Projectile>().stun = stun;
                        proj.GetComponent<Projectile>().burnDamage = burnDamage;
                        proj.GetComponent<Projectile>().homing = homing;
                        angle += 360f / (shockwaveProjectiles * multishot);
                    }
                }
                else if(playerType == "lightning"){
                    //instantiate a projectile for every enemy and make it point towards said enemy
                    float chains = 0f;
                    foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
                        float distance = Vector2.Distance(firePoint.position, enemy.transform.position);
                        if(chains >= 5){
                            return;
                        }
                        if(distance <= processedRange){
                            chains++;
                            proj = Instantiate(projectile, firePoint.position, Quaternion.identity);
                            Vector3 dir = enemy.transform.position - firePoint.position;
                            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                            proj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                            proj.GetComponent<Projectile>().target = enemy.transform;
                            proj.GetComponent<Projectile>().damage = processedDamage / 25f;
                            proj.GetComponent<Projectile>().speed = processedProjectileSpeed;
                            proj.GetComponent<Projectile>().playerScript = this;
                            proj.GetComponent<Projectile>().freezeEffect = freezeEffect;
                            proj.GetComponent<Projectile>().stun = stun;
                            proj.GetComponent<Projectile>().burnDamage = burnDamage;
                            proj.GetComponent<Projectile>().homing = homing;
                        }
                    }
                    return;
                }
                else{
                    proj = Instantiate(projectile, firePoint.position, Quaternion.identity);
                }
                if(proj == null){
                    break;
                }
                proj.GetComponent<Projectile>().target = GetClosestEnemy(this.transform);
                proj.GetComponent<Projectile>().damage = processedDamage;
                proj.GetComponent<Projectile>().speed = processedProjectileSpeed;
                proj.GetComponent<Projectile>().playerScript = this;
                proj.GetComponent<Projectile>().freezeEffect = freezeEffect;
                proj.GetComponent<Projectile>().stun = stun;
                proj.GetComponent<Projectile>().burnDamage = burnDamage;
                proj.GetComponent<Projectile>().homing = homing;
            }
        }
    }

    public Transform GetClosestEnemy(Transform startPoint){
        Transform targetEnemy = null;
        float furthestDistance = 0;
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
            float distance = Vector2.Distance(startPoint.position, enemy.transform.position);
            if(distance <= processedRange){
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
        //update processedRange
        processedRange = range * rangePercent / 100f;
        if(playerType == "lazer"){
            lazerRot += (1f / updateSpeed) * lazerRotSpeed;
            if(lazerRot > 360f){
                lazerRot -= 360f;
            }
        }
        //update range indicator
        rangeIndicator.transform.localScale = new Vector3(processedRange * 1.9f, processedRange * 1.9f, 1);
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
