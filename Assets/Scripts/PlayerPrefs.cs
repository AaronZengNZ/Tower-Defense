using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefs : MonoBehaviour
{
    public float startGold = 0f;
    public MoneyScript moneyScript;
    public float damageBoost = 0f;
    public float rangeBoost = 0f;
    public float firerateBoost = 0f;
    public float projSpeedBoost = 0f;
    public PlayerBoosts playerBoosts;
    void Awake()
    {
        //find moneyscript
        moneyScript = GameObject.Find("Money Manager").GetComponent<MoneyScript>();
        if (GameObject.FindGameObjectsWithTag("PlayerPrefs").Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        if(CheckIfPrefExists("Gold") == false){
            CreatePref("Gold", startGold);
        }
        else{
            moneyScript.moneys = GetFloatPref("Gold");
        }
        CheckPrefAndCreate("LevelsBeat", 0f);

        CheckPrefAndCreate("DamageBoost", 0f);
        CheckPrefAndCreate("RangeBoost", 0f);
        CheckPrefAndCreate("FirerateBoost", 0f);
        CheckPrefAndCreate("ProjSpeedBoost", 0f);
        playerBoosts = GameObject.Find("PlayerBoosts").GetComponent<PlayerBoosts>();
        playerBoosts.damageBoost = GetFloatPref("DamageBoost");
        playerBoosts.rangeBoost = GetFloatPref("RangeBoost");
        playerBoosts.firerateBoost = GetFloatPref("FirerateBoost");
        playerBoosts.projSpeedBoost = GetFloatPref("ProjSpeedBoost");
        UpdateSelfBoostsToPrefValues();
    }

    void Update(){
        UpdateSelfBoostsToPrefValues();
        UpdatePlayerBoostToPrefValues();
    }

    public bool CheckPrefAndCreate(string key, float startingValue){
        if(CheckIfPrefExists(key) == false){
            UnityEngine.Debug.Log("Creating pref: " + key + " with value: " + startingValue);
            CreatePref(key, startingValue);
            return false;
        }
        else{
            UnityEngine.Debug.Log("Pref: " + key + " already exists with value: " + GetFloatPref(key));
        }
        return true;
    }

    public bool CheckIfPrefExists(string key){
        return UnityEngine.PlayerPrefs.HasKey(key);
    }
    public void UpdateSelfBoostsToPrefValues(){
        damageBoost = GetFloatPref("DamageBoost");
        rangeBoost = GetFloatPref("RangeBoost");
        firerateBoost = GetFloatPref("FirerateBoost");
        projSpeedBoost = GetFloatPref("ProjSpeedBoost");
    }
    public void UpdatePlayerBoostToPrefValues(){
        playerBoosts.damageBoost = GetFloatPref("DamageBoost");
        playerBoosts.rangeBoost = GetFloatPref("RangeBoost");
        playerBoosts.firerateBoost = GetFloatPref("FirerateBoost");
        playerBoosts.projSpeedBoost = GetFloatPref("ProjSpeedBoost");
    }
    public void BeatLevel(float level){
        if(GetFloatPref("LevelsBeat") < level){
            SetPref("LevelsBeat", level);
        }
    }
    public void SetPref(string key, float amount){
        if(CheckIfPrefExists(key)){
            UnityEngine.PlayerPrefs.SetFloat(key, amount);
        }
    }
    public void SetPref(string key, string amount){
        if(CheckIfPrefExists(key)){
            UnityEngine.PlayerPrefs.SetString(key, amount);
        }
    }
    public void CreatePref(string key, float startingValue){
        if(CheckIfPrefExists(key) == false){
            UnityEngine.PlayerPrefs.SetFloat(key, startingValue);
        }
    }
    public float GetFloatPref(string key){
        if(CheckIfPrefExists(key)){
            return UnityEngine.PlayerPrefs.GetFloat(key);
        }
        return 0f;
    }
    public string GetStringPref(string key){
        if(CheckIfPrefExists(key)){
            return UnityEngine.PlayerPrefs.GetString(key);
        }
        return "";
    }

    public bool BuyItem(float cost){
        if(moneyScript.moneys >= cost){
            moneyScript.moneys -= cost;
            SetPref("Gold", moneyScript.moneys);
            return true;
        }
        return false;
    }
    public void Erase(){
        UnityEngine.PlayerPrefs.DeleteAll();
        //call moneyscript to erase()
        moneyScript.Erase();
        if (GameObject.FindGameObjectsWithTag("PlayerPrefs").Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        if(CheckIfPrefExists("Gold") == false){
            CreatePref("Gold", startGold);
        }
        else{
            moneyScript.moneys = GetFloatPref("Gold");
        }
        CheckPrefAndCreate("LevelsBeat", 0f);

        CheckPrefAndCreate("DamageBoost", 0f);
        CheckPrefAndCreate("RangeBoost", 0f);
        CheckPrefAndCreate("FirerateBoost", 0f);
        CheckPrefAndCreate("ProjSpeedBoost", 0f);
        playerBoosts = GameObject.Find("PlayerBoosts").GetComponent<PlayerBoosts>();
        playerBoosts.damageBoost = GetFloatPref("DamageBoost");
        playerBoosts.rangeBoost = GetFloatPref("RangeBoost");
        playerBoosts.firerateBoost = GetFloatPref("FirerateBoost");
        playerBoosts.projSpeedBoost = GetFloatPref("ProjSpeedBoost");
        UpdateSelfBoostsToPrefValues();
    }
}
