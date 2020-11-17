using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    SpriteRenderer renderer;
    public GameObject caution;
    Vector4 tempColor;
    int num = 3;
    float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = caution.transform.GetComponent<SpriteRenderer>();

        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        Color color = renderer.color;
        tempColor = color;
        for(int i =0; i<num;i++)
        {
            for(float j = 0f; j <=0.8f; j -= speed * Time.deltaTime)
            {
                tempColor.w = j;
                yield return null;
            }
            tempColor.w = 0;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        Color color = renderer.color;
        tempColor = color;
        for (int i = 0; i < num; i++)
        {
            for (float j = 0f; j <= 0.8f; j += speed * Time.deltaTime)
            {
                tempColor.w = j;
                yield return null;
            }
            tempColor.w = 0.8f;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine("FadeOut");
    }

    private void LateUpdate()
    {
        renderer.color = tempColor;
    }
}
