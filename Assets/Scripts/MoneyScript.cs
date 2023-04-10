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
    // Start is called before the first frame update
    void Start()
    {
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
        moneyText.text = moneys.ToString();
    }
}
