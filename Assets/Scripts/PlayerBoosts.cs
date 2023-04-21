using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoosts : MonoBehaviour
{
    public float damageBoost = 0f;
    public float rangeBoost = 0f;
    public float firerateBoost = 0f;
    public float projSpeedBoost = 0f;
    public PlayerPrefs playerPrefs;
    void Awake()
    {
        //dont destroy if no other, else if other exist, destroy
        if(FindObjectsOfType(GetType()).Length > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
        playerPrefs = GameObject.Find("PlayerPrefs").GetComponent<PlayerPrefs>();
    }

    public void UpdatePlayerPrefs(){
        playerPrefs.SetPref("DamageBoost", damageBoost);
        playerPrefs.SetPref("RangeBoost", rangeBoost);
        playerPrefs.SetPref("FirerateBoost", firerateBoost);
        playerPrefs.SetPref("ProjSpeedBoost", projSpeedBoost);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null){
            player.GetComponent<Player>().damagePercent = 100 + damageBoost;
            player.GetComponent<Player>().rangePercent = 100 + rangeBoost;
            player.GetComponent<Player>().fireratePercent = 100 + firerateBoost;
            player.GetComponent<Player>().bulletSpeedPercent = 100 + projSpeedBoost;
        }
    }
}
