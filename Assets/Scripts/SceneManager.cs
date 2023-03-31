using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public void NextScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
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
