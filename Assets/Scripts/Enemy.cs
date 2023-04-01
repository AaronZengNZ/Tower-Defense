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
    public float moneyOnDeath = 1f;
    public GameObject sprite;
    public GameObject deathCoin;
    public float coinSpewForce = 3f;
    public float burningDamage = 1f;
    public float burningTimeLeft = 0f;
    bool stunned = false;
    public float freezeStacks = 0f;
    public ParticleSystem fireEffect;
    public SceneManager sceneManagerScript;
    public GameObject damageText;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[0].transform.position;
        StartCoroutine(BurnCoroutine());
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    public void Burn(float damage){
        burningTimeLeft = 3f;
        burningDamage = damage;
    }

    IEnumerator BurnCoroutine(){
        while(true){
            if(burningTimeLeft > 0){
                TakeDamage(burningDamage);
                GameObject damageTextInstance = Instantiate(damageText, transform.position, Quaternion.identity);
                damageTextInstance.GetComponent<DamageText>().number = burningDamage;
                burningTimeLeft -= 0.3f;
                fireEffect.Play();
            }
            yield return new WaitForSeconds(0.3f);
        }
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

    public void Stun(float time){
        if(stunned == false){
            StartCoroutine(StunCoroutine(time));
        }
    }

    IEnumerator StunCoroutine(float time){
        stunned = true;
        float temp = moveSpeed;
        moveSpeed = 0f;
        yield return new WaitForSeconds(time);
        moveSpeed = temp;
        stunned = false;
    }

    public void Freeze(float amount){
        if(freezeStacks < 5){
            moveSpeed = moveSpeed / (1 + amount/100);
            freezeStacks++;
        }
    }

    public void Move(){
        if(currentWaypoint < waypoints.Length){
            transform.position = Vector2.MoveTowards(transform.position, waypoints[(int)currentWaypoint].transform.position, moveSpeed * UnityEngine.Time.deltaTime);
            //if distance to waypoint is less than 0.1f, go to next waypoint
            if(Vector2.Distance(transform.position, waypoints[(int)currentWaypoint].transform.position) < 0.1f){
                currentWaypoint += 1f;
            }
            //flip sprite according to waypoint direction
            if(currentWaypoint < waypoints.Length){
            if(waypoints[(int)currentWaypoint] != null){
                if(waypoints[(int)currentWaypoint].transform.position.x > transform.position.x){
                    sprite.transform.localScale = new Vector3(1, 1, 1);
                }
                else if(waypoints[(int)currentWaypoint].transform.position.x < transform.position.x){
                    sprite.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            }
        }
        else{
            moneyOnDeath = 0f;
            //call lose
            sceneManagerScript.Lose();
            Destroy(gameObject);
        }
    }

    void OnDestroy(){
        //instantiate moneyOnDeath deathCoins
        for(int i = 0; i < moneyOnDeath; i++){
            GameObject coin = Instantiate(deathCoin, transform.position, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * coinSpewForce);
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
