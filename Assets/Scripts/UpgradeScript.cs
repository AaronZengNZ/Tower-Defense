using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeScript : MonoBehaviour
{
    public Player playerScript;
    public GameObject shopCanvas;
    public GameObject[] upgrades;

    public GameObject[] commonUpgradeSelection;
    public GameObject[] rareUpgradeSelection;
    public GameObject[] epicUpgradeSelection;
    public GameObject[] legendaryUpgradeSelection;

    public Transform wayPoint1;
    public Transform wayPoint2;
    public Transform wayPoint3;
    public Canvas canvas;
    public GameObject[] alreadySelected;
    public EnemySpawner spawner;

    void Start(){
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    public void InstantiateUpgrades(float rarity){
        //activate shopCanvas
        shopCanvas.SetActive(true);
        //make an array of 3 randoms (float) with the equation UnityEngine.Random.Range(0f, 1f) * rarity for each of them
        float[] randoms = new float[3];
        string[] rarities = new string[3];
        UnityEngine.Debug.Log(rarity);
        for(int i = 0; i < randoms.Length; i++){
            randoms[i] = UnityEngine.Random.Range(0f, 1f) * rarity;
        }
        for(int i = 0; i < randoms.Length; i++){
            UnityEngine.Debug.Log(randoms[i]);
            if(randoms[i] <= 10){
                rarities[i] = "common";
            }
            else if(randoms[i] <= 15){
                rarities[i] = "rare";
            }
            else if(randoms[i] < 19){
                rarities[i] = "epic";
            }
            else{
                rarities[i] = "legendary";
            }
        }
        GameObject upgrade1 = Instantiate(GetUpgrade(rarities[0]), wayPoint1.position, Quaternion.identity, wayPoint1);
        GameObject upgrade2 = Instantiate(GetUpgrade(rarities[1]), wayPoint2.position, Quaternion.identity, wayPoint2);
        GameObject upgrade3 = Instantiate(GetUpgrade(rarities[2]), wayPoint3.position, Quaternion.identity, wayPoint3);
        upgrades = new GameObject[]{upgrade1, upgrade2, upgrade3};
    }

    public GameObject GetUpgrade(string rarity){
        if(rarity == "common"){
            GameObject selected = commonUpgradeSelection[UnityEngine.Random.Range(0, commonUpgradeSelection.Length)];
            //if alreadySelected contains selected
            if(alreadySelected.Length > 0){
                for(int i = 0; i < alreadySelected.Length; i++){
                    if(alreadySelected[i] == selected){
                        selected = commonUpgradeSelection[UnityEngine.Random.Range(0, commonUpgradeSelection.Length)];
                    }
                }
            }
            GameObject[] previous = alreadySelected;
            alreadySelected = new GameObject[alreadySelected.Length + 1];
            for(int i = 0; i < previous.Length; i++){
                alreadySelected[i] = previous[i];
            }
            alreadySelected[alreadySelected.Length - 1] = selected;
            return selected;
        }
        if(rarity == "rare"){
            GameObject selected = rareUpgradeSelection[UnityEngine.Random.Range(0, rareUpgradeSelection.Length)];
            //if alreadySelected contains selected
            if(alreadySelected.Length > 0){
                for(int i = 0; i < alreadySelected.Length; i++){
                    if(alreadySelected[i] == selected){
                        selected = rareUpgradeSelection[UnityEngine.Random.Range(0, rareUpgradeSelection.Length)];
                    }
                }
            }
            GameObject[] previous = alreadySelected;
            alreadySelected = new GameObject[alreadySelected.Length + 1];
            for(int i = 0; i < previous.Length; i++){
                alreadySelected[i] = previous[i];
            }
            return selected;
        }
        if(rarity == "epic"){
            GameObject selected = epicUpgradeSelection[UnityEngine.Random.Range(0, epicUpgradeSelection.Length)];
            //if alreadySelected contains selected
            if(alreadySelected.Length > 0){
                for(int i = 0; i < alreadySelected.Length; i++){
                    if(alreadySelected[i] == selected){
                        selected = epicUpgradeSelection[UnityEngine.Random.Range(0, epicUpgradeSelection.Length)];
                    }
                }
            }
            GameObject[] previous = alreadySelected;
            alreadySelected = new GameObject[alreadySelected.Length + 1];
            for(int i = 0; i < previous.Length; i++){
                alreadySelected[i] = previous[i];
            }
            return selected;
        }
        if(rarity == "legendary"){
            GameObject selected = legendaryUpgradeSelection[UnityEngine.Random.Range(0, legendaryUpgradeSelection.Length)];
            //if alreadySelected contains selected
            if(alreadySelected.Length > 0){
                for(int i = 0; i < alreadySelected.Length; i++){
                    if(alreadySelected[i] == selected){
                        selected = legendaryUpgradeSelection[UnityEngine.Random.Range(0, legendaryUpgradeSelection.Length)];
                    }
                }
            }
            GameObject[] previous = alreadySelected;
            alreadySelected = new GameObject[alreadySelected.Length + 1];
            for(int i = 0; i < previous.Length; i++){
                alreadySelected[i] = previous[i];
            }
            return selected;
        }
        return null;
    }

    public void HandleUpgrade(string upgrade, float amount, string debuff, float power, bool hasDebuff, GameObject upgradeSelected, string rarity){
        if(upgrade == "damage"){
            playerScript.damage += amount;
        }
        if(upgrade == "movement"){
            playerScript.moveSpeed += amount;
        }
        if(upgrade == "firerate"){
            playerScript.shootSpeed += amount;
        }
        if(upgrade == "range"){
            playerScript.range += amount;
        }
        if(upgrade == "speed"){
            playerScript.projectileSpeed += amount;
        }
        if(upgrade == "freeze"){
            playerScript.freezeEffect += amount;
        }
        //add stun
        if(upgrade == "stun"){
            playerScript.stun += amount;
        }
        if(upgrade == "fire"){
            playerScript.burnDamage += amount;
        }
        if(upgrade == "homing"){
            playerScript.homing = true;
        }
        //debuffs
        if(hasDebuff == true){
            if(debuff == "damage"){
                playerScript.damage = playerScript.damage * (1 - power);
            }
            if(debuff == "movement"){
                playerScript.moveSpeed = playerScript.moveSpeed * (1 - power);
            }
            if(debuff == "firerate"){
                playerScript.shootSpeed = playerScript.shootSpeed * (1 - power);
            }
            if(debuff == "range"){
                playerScript.range =   playerScript.range * (1 - power);
            }
            if(debuff == "speed"){
                playerScript.projectileSpeed = playerScript.projectileSpeed * (1 - power);
            }
        }
        //if the upgrade has it's onebuy bool as true, delete the upgrade from the correct array
        if(upgradeSelected.GetComponent<Upgrade>().oneBuy == true){
            if(rarity == "common"){
                GameObject[] previous = commonUpgradeSelection;
                commonUpgradeSelection = new GameObject[commonUpgradeSelection.Length - 1];
                float temp = 0f;
                for(int i = 0; i < commonUpgradeSelection.Length; i++){
                    if(previous[i] != upgradeSelected){
                        commonUpgradeSelection[i] = previous[(int)temp];
                        temp++;
                    }
                }
            }
            if(rarity == "rare"){
                GameObject[] previous = rareUpgradeSelection;
                rareUpgradeSelection = new GameObject[rareUpgradeSelection.Length - 1];
                float temp = 0f;
                for(int i = 0; i < rareUpgradeSelection.Length; i++){
                    if(previous[i] != upgradeSelected){
                        rareUpgradeSelection[i] = previous[(int)temp];
                        temp++;
                    }
                }
            }
            if(rarity == "epic"){
                GameObject[] previous = epicUpgradeSelection;
                epicUpgradeSelection = new GameObject[epicUpgradeSelection.Length - 1];
                float temp = 0f;
                for(int i = 0; i < epicUpgradeSelection.Length; i++){
                    if(previous[i] != upgradeSelected){
                        epicUpgradeSelection[i] = previous[(int)temp];
                        temp++;
                    }
                }
            }
            if(rarity == "legendary"){
                GameObject[] previous = legendaryUpgradeSelection;
                legendaryUpgradeSelection = new GameObject[legendaryUpgradeSelection.Length - 1];
                float temp = 0f;
                for(int i = 0; i < legendaryUpgradeSelection.Length; i++){
                    if(previous[i] != upgradeSelected){
                        legendaryUpgradeSelection[i] = previous[(int)temp];
                        temp++;
                    }
                }
            }
        }

        
        EndUpgrade();
    }

    public void EndUpgrade(){
        //clear arrays
        alreadySelected = new GameObject[0];
        shopCanvas.SetActive(false);
        foreach(GameObject upgrade in upgrades){
            Destroy(upgrade);
        }
        upgrades = new GameObject[0];
        //spawn enemies
        spawner.NextWave();
    }
}
