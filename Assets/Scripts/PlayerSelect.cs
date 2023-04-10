using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public MoneyScript moneyScript;
    public GameObject[] selecteds;
    void Start(){
        moneyScript = GameObject.Find("Money Manager").GetComponent<MoneyScript>();
        for(int i = 1; i < selecteds.Length; i++){
            selecteds[i].SetActive(false);
        }
        moneyScript.playerNum = 1;
    }

    public void Select(float num){
        moneyScript.playerNum = num;
        for(int i = 0; i < selecteds.Length; i++){
            selecteds[i].SetActive(false);
        }
        selecteds[(int)moneyScript.playerNum - 1].SetActive(true);
    }
}
