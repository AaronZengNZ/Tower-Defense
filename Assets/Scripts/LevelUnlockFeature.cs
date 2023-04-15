using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlockFeature : MonoBehaviour
{
    public GameObject feature;
    public bool hideOnUnlock = true;
    public float levelRequirement = 1f;
    public PlayerPrefs prefScript;
    // Start is called before the first frame update
    void Start()
    {
        //check
        if(hideOnUnlock){
                feature.SetActive(true);
        }
        else{
            feature.SetActive(false);
        }
        prefScript = GameObject.Find("PlayerPrefs").GetComponent<PlayerPrefs>();
        if(prefScript.GetFloatPref("LevelsBeat") >= levelRequirement){
            if(hideOnUnlock){
                feature.SetActive(false);
            }
            else{
                feature.SetActive(true);
            }
        }
    }
}
