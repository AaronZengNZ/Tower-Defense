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

    public void InstantiateUpgrades(string rarity1, string rarity2, string rarity3){
        //activate shopCanvas
        shopCanvas.SetActive(true);
        GameObject upgrade1 = Instantiate(GetUpgrade(rarity1), wayPoint1.position, Quaternion.identity, wayPoint1);
        GameObject upgrade2 = Instantiate(GetUpgrade(rarity2), wayPoint2.position, Quaternion.identity, wayPoint2);
        GameObject upgrade3 = Instantiate(GetUpgrade(rarity3), wayPoint3.position, Quaternion.identity, wayPoint3);
        upgrades = new GameObject[]{upgrade1, upgrade2, upgrade3};
    }

    public GameObject GetUpgrade(string rarity){
        if(rarity == "common"){
            GameObject selected = commonUpgradeSelection[Random.Range(0, commonUpgradeSelection.Length)];
            //if alreadySelected contains selected
            if(alreadySelected.Length > 0){
                for(int i = 0; i < alreadySelected.Length; i++){
                    if(alreadySelected[i] == selected){
                        selected = commonUpgradeSelection[Random.Range(0, commonUpgradeSelection.Length)];
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
            GameObject selected = rareUpgradeSelection[Random.Range(0, rareUpgradeSelection.Length)];
            //if alreadySelected contains selected
            if(alreadySelected.Length > 0){
                for(int i = 0; i < alreadySelected.Length; i++){
                    if(alreadySelected[i] == selected){
                        selected = rareUpgradeSelection[Random.Range(0, rareUpgradeSelection.Length)];
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
            GameObject selected = epicUpgradeSelection[Random.Range(0, epicUpgradeSelection.Length)];
            //if alreadySelected contains selected
            if(alreadySelected.Length > 0){
                for(int i = 0; i < alreadySelected.Length; i++){
                    if(alreadySelected[i] == selected){
                        selected = epicUpgradeSelection[Random.Range(0, epicUpgradeSelection.Length)];
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
            GameObject selected = legendaryUpgradeSelection[Random.Range(0, legendaryUpgradeSelection.Length)];
            //if alreadySelected contains selected
            if(alreadySelected.Length > 0){
                for(int i = 0; i < alreadySelected.Length; i++){
                    if(alreadySelected[i] == selected){
                        selected = legendaryUpgradeSelection[Random.Range(0, legendaryUpgradeSelection.Length)];
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

    public void HandleUpgrade(string upgrade, float amount, string debuff, float power, bool hasDebuff){
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
        //debuffs
        if(hasDebuff == true){
            if(debuff == "damage"){
                playerScript.damage -= power;
            }
            if(debuff == "movement"){
                playerScript.moveSpeed -= power;
            }
            if(debuff == "firerate"){
                playerScript.shootSpeed -= power;
            }
            if(debuff == "range"){
                playerScript.range -= power;
            }
            if(debuff == "speed"){
                playerScript.projectileSpeed -= power;
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
