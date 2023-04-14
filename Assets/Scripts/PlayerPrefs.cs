using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs : MonoBehaviour
{
    public float startGold = 0f;
    public MoneyScript moneyScript;
    public bool Override0 = true;
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
    }

    public bool CheckIfPrefExists(string key){
        return UnityEngine.PlayerPrefs.HasKey(key);
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
}
