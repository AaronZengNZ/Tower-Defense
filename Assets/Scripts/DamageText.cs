using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public Rigidbody2D rb;
    public float upwardsForce = 2f;
    public float number = 0f;
    public TextMeshProUGUI text;
    public float fadeSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.up * upwardsForce, ForceMode2D.Impulse);
        StartCoroutine(FadeAway());
    }
    void Update(){
        text.text = UnityEngine.Mathf.Ceil(number).ToString();
    }

    IEnumerator FadeAway(){
        yield return new WaitForSeconds(0.3f);
        while(text.color.a > 0){
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - fadeSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
