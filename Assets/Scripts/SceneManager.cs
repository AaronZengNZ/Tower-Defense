using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public Fade fadeScript;
    bool fading = false;
    int thisScene = 0;
    public void GoToScene(int scene){
        if(fading == false){
        fading = true;
        thisScene = scene;
        StartCoroutine(fadeNextScene());
        }   
    }

    IEnumerator fadeNextScene(){
        fadeScript.FadeOut();
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(thisScene);
        UnityEngine.Time.timeScale = 1;
    }
    public void GoToFirstScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        UnityEngine.Time.timeScale = 1;
    }
    public void Win(){
        winCanvas.SetActive(true);
        UnityEngine.Time.timeScale = 0;
    }
    public void Lose(){
        loseCanvas.SetActive(true);
        UnityEngine.Time.timeScale = 0;
    }
}
