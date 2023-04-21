using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public void Erase(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        moneys = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if(prevMoneys != moneys){
            playerPrefs.SetPref("Gold", moneys);
        }
        prevMoneys = moneys;
        moneyText.text = moneys.ToString();
        //if cost is over 1000, divide by 1000 and add k (round to 1 digit, floor)
        if(moneys > 1000f){
            moneyText.text = (Mathf.Floor(moneys / 100f) / 10f).ToString() + "k";
        }
        //if cost over 100000, round with no decimals (floor)
        if(moneys > 100000f){
            moneyText.text = Mathf.Floor(moneys / 1000f).ToString() + "k";
        }
    }
}
