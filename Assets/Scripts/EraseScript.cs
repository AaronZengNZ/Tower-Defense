using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseScript : MonoBehaviour
{
    public PlayerPrefs playerPrefs;
    // Start is called before the first frame update
    void Start()
    {
        playerPrefs = GameObject.Find("PlayerPrefs").GetComponent<PlayerPrefs>();
    }

    public void CallErase(){
        playerPrefs.Erase();
    }
}
