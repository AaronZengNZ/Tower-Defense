using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    public Image fadeImage;
    public bool fadeIn = false;
    public float fadeDuration = 1f;
    void Start()
    {fadeImage.gameObject.SetActive(false);
        StartCoroutine(FadeImage());
    }

    IEnumerator FadeImage(){
        if(fadeIn){
            //get fadeimage's gameobject and set it to inactive
            fadeImage.gameObject.SetActive(true);
            for(float i = 1; i >= 0; i -= UnityEngine.Time.deltaTime / fadeDuration){
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
            fadeImage.gameObject.SetActive(false);
        }
    }

    public void FadeOut(){
        StartCoroutine(FadeImageOut());
    }

    IEnumerator FadeImageOut(){
        fadeImage.gameObject.SetActive(true);
        for(float i = 0; i <= 1; i += UnityEngine.Time.deltaTime / fadeDuration){
            fadeImage.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }
}
