using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgrade : MonoBehaviour
{
    public string effect = "Damage";
    public float boostAmount = 0f;
    public float cost = 100f;
    public TextMeshProUGUI costText;
    public PlayerPrefs playerPrefs;
    public string name = "DamageUpgrade";
    // Start is called before the first frame update
    void Start()
    {
        playerPrefs = GameObject.Find("PlayerPrefs").GetComponent<PlayerPrefs>();
        if(playerPrefs.CheckPrefAndCreate(name + "Cost", cost) == true){
            cost = playerPrefs.GetFloatPref(name + "Cost");
        }
    }

    public void BuyUpgrade(){
        UnityEngine.Debug.Log("tryBuy");
        if(playerPrefs.BuyItem(cost)){
            UnityEngine.Debug.Log("success");
            playerPrefs.SetPref(effect + "Boost", playerPrefs.GetFloatPref(effect + "Boost") + boostAmount);
            if(cost * 1.25f > cost + 100f){
                cost = cost * 1.2f;
                cost = Mathf.Round(cost / 10f) * 10f;
            }
            else{
                cost = cost + 100f;
            }
            playerPrefs.SetPref(name + "Cost", cost);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if playerPrefs has not been found, find playerPrefs
        if(playerPrefs == null){
            playerPrefs = GameObject.Find("PlayerPrefs").GetComponent<PlayerPrefs>();
            //if playerprefs has been found, check cost
            if(playerPrefs != null){
                if(playerPrefs.CheckPrefAndCreate(name + "Cost", cost) == true){
                    cost = playerPrefs.GetFloatPref(name + "Cost");
                }
            }
        }
        costText.text = cost.ToString();
        //if cost is over 1000, divide by 1000 and add k (round to 1 digit, cieling)
        if(cost > 1000f){
            costText.text = (Mathf.Ceil(cost / 100f) / 10f).ToString() + "k";
        }
        //if cost over 100000, round with no decimals (cieling)
        if(cost > 100000f){
            costText.text = Mathf.Ceil(cost / 1000f).ToString() + "k";
        }
    }
}
