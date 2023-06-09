using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed = 25f;
    public float damage = 1f;
    public float freezeEffect = 0f;
    public float stun = 0f;
    public Player playerScript;
    public CircleCollider2D circleCollider;
    public float turnSpeed = 5f;
    public Rigidbody2D rb;
    //get the sprite renderer
    public SpriteRenderer spriteRenderer;

    public bool homing = false;
    public float noTurning = 10f;
    public float burnDamage = 0f;
    public GameObject damageText;
    public bool noTurn = false;
    float life = 100f;
    public float colliderActivate = 3f;
    public bool lightning = false;
    public GameObject[] enemiesChained;
    public TrailRenderer trail;
    public float chainDistance = 20f;
    public GameObject lightningTarget;

    void Start(){
        //set size to damage
        float size;
        circleCollider.enabled = false;
        //make size the log3 of damage
        size = ((float)Math.Log((damage+7), 10));
        transform.localScale = new Vector3(-size, size, 1);
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ManualUpdateAutomator());
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(noTurn == false){
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
        
    }
    void Update(){
        if(lightning){
            GameObject player = GameObject.FindWithTag("Player");
            if(Vector2.Distance(transform.position, player.transform.position) > playerScript.range){
                Destroy(gameObject);
            }
        }
    }
    void ManualUpdate()
    {
        if(noTurn == true){
            rb.velocity = transform.right * speed;
            if(colliderActivate <= 0){
                circleCollider.enabled = true;
            }
            else{
                colliderActivate --;
            }
            life -= 1;
            if(life <= 0){
                Destroy(gameObject);
            }
            return;
        }
        if(noTurning >= 0){
            noTurning -= speed;
            rb.velocity = transform.right * speed;
            return;
        }
        if(noTurning <= 0 && noTurning > -1000){
            noTurning = -10000;
            circleCollider.enabled = true;
        }
        //rb.velocity = rb.velocity / 1.2f;
        if(target == null){
            if(homing == true){
                target = playerScript.GetClosestEnemy(this.transform);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.025f);
                if(spriteRenderer.color.a <= 0){
                    Destroy(gameObject);
                }
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
            //full opacity
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
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
     void OnDestroy()
    {
        if(lightning){
            trail.transform.parent = null;
            trail.autodestruct = true;
            trail = null;
        }
    }

    // on collision with tag enemy, hit target
    //change the oncollisionenter to a ontriggerenter
    private void OnTriggerEnter2D(Collider2D other) {
        if(target == null || other.gameObject == null && homing == true){
            return;
        }
        if(other.gameObject.tag == "Enemy"){
            //do a null check on target and other's gameObjects
            if(noTurn == false && other.gameObject != target.gameObject){
                return;
            }
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            //instantiate a damage text at 0 degrees rotation
            GameObject damageTextInstance = Instantiate(damageText, this.transform.position, Quaternion.Euler(0, 0, 0));
            damageTextInstance.GetComponent<DamageText>().number = damage;
            other.gameObject.GetComponent<Enemy>().Freeze(freezeEffect);
            if(stun > 0){
                other.gameObject.GetComponent<Enemy>().Stun(stun);
            }
            if(burnDamage > 0){
                other.gameObject.GetComponent<Enemy>().Burn(burnDamage);
            }
            Destroy(gameObject);
        }
    }
}
