using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public Fade fadeScript;
    bool fading = false;
    int thisScene = 0;
    public float lives = -100f;
    public TextMeshProUGUI livesText;

    public void GoToScene(int scene){
        if(fading == false){
        fading = true;
        thisScene = scene;
        StartCoroutine(fadeNextScene());
        }   
    }
    void Update(){
        if(lives > -1f){
            livesText.text = lives.ToString();
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
        //find playerprefs script and call beatlevel with the current scene number
        PlayerPrefs playerPrefs = GameObject.Find("PlayerPrefs").GetComponent<PlayerPrefs>();
        playerPrefs.BeatLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void Lose(float dmg){
        //decrease hp by dmg squared / 10 cieling
        lives -= Mathf.Ceil(dmg * dmg / 10f);
        if(lives <= 0){
            loseCanvas.SetActive(true);
            UnityEngine.Time.timeScale = 0;
        }
    }
}
