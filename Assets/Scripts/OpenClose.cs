using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target.SetActive(false);
    }

    public void Open(){
        target.SetActive(true);
    }
    public void Close(){
        target.SetActive(false);
    }
}
