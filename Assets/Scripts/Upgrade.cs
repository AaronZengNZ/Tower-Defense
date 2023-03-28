using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string buffEffect;
    public string debuffEffect;
    public float buffAmount;
    public float debuffAmount;
    public bool hasDebuff = false;
    public UpgradeScript upgradeManager;
    public bool oneBuy = false;
    public string rarity = "common";
    void Start(){
        upgradeManager = GameObject.Find("Upgrade Manager").GetComponent<UpgradeScript>();
    }
    
    public void TakeUpgrade(){
        upgradeManager.HandleUpgrade(buffEffect, buffAmount, debuffEffect, debuffAmount, hasDebuff, this.gameObject, rarity);
    }
}
