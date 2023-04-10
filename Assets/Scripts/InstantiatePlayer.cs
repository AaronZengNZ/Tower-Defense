using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //find moneyScript, get player, instantiate player at this location
        MoneyScript moneyScript = GameObject.Find("Money Manager").GetComponent<MoneyScript>();
        GameObject player = moneyScript.getPlayer();
        Instantiate(player, transform.position, transform.rotation);
    }
}
