using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyScript : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public float moneys;
    public GameObject[] players;
    public float playerNum = 1f;
    public PlayerPrefs playerPrefs;
    float prevMoneys;
    // Start is called before the first frame update
    void Start()
    {
        playerPrefs = GameObject.Find("PlayerPrefs").GetComponent<PlayerPrefs>();
        if(FindObjectsOfType<MoneyScript>().Length > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }
    public GameObject getPlayer(){
        return players[(int)playerNum - 1];
    }
    // Update is called once per frame
    void Update()
    {
        if(prevMoneys != moneys){
            playerPrefs.SetPref("Gold", moneys);
        }
        prevMoneys = moneys;
        moneyText.text = moneys.ToString();
    }
}
