using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sample : MonoBehaviour
{
    SpriteRenderer rer;
    float duration = 5; // 
    float smoothness = 0.05f; // 

    void Start()
    {
        rer = GetComponent<SpriteRenderer>();
        StartCoroutine("LerpColor");
    }
    IEnumerator LerpColor() // 자연스럽게 점차 색바꾸기
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            rer.color = Color.Lerp(Color.gray, Color.white, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        yield return true;
    }
}
