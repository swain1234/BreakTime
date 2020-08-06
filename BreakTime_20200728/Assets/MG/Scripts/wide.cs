using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wide : MonoBehaviour
{

    [SerializeField] Image upImage;
    [SerializeField] Image downImage;
    [SerializeField] Image leftImage;
    [SerializeField] Image rightImage;


    float smoothness = 0.05f; // 
    float duration = 0.01f; // 
    // Start is called before the first frame update
    void Start()
    {
        upImage = upImage.GetComponent<Image>();
        downImage = downImage.GetComponent<Image>();
        leftImage = leftImage.GetComponent<Image>();
        rightImage = rightImage.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            StartCoroutine(WideChange());
        }
        if (Input.GetKeyDown("w"))
        {
            StartCoroutine(NarrowChange());
        }
    }

    IEnumerator WideChange()
    {
        float progress = upImage.rectTransform.sizeDelta.y; ;
        float increment = smoothness / duration;

        if (progress > 0)
        {
            do
            {
                upImage.rectTransform.sizeDelta = new Vector2(upImage.rectTransform.sizeDelta.x, progress);
                downImage.rectTransform.sizeDelta = new Vector2(downImage.rectTransform.sizeDelta.x, progress);
                progress -= increment;
                yield return new WaitForSeconds(smoothness);
            } while (progress > -20);
        }
        else
        {
            do
            {
                upImage.rectTransform.sizeDelta = new Vector2(upImage.rectTransform.sizeDelta.x, progress);
                downImage.rectTransform.sizeDelta = new Vector2(downImage.rectTransform.sizeDelta.x, progress);
                progress += increment;
                yield return new WaitForSeconds(smoothness);
            } while (progress < 200);
        }
        yield return true;
    }
    IEnumerator NarrowChange()
    {
        float progress = leftImage.rectTransform.sizeDelta.x; ;
        float increment = smoothness / duration;

        if (progress > 0)
        {
            do
            {
                leftImage.rectTransform.sizeDelta = new Vector2(progress, leftImage.rectTransform.sizeDelta.y);
                rightImage.rectTransform.sizeDelta = new Vector2(progress, rightImage.rectTransform.sizeDelta.y);
                progress -= increment;
                yield return new WaitForSeconds(smoothness);
            } while (progress > -50);
        }
        else
        {
            do
            {
                leftImage.rectTransform.sizeDelta = new Vector2(progress, leftImage.rectTransform.sizeDelta.y);
                rightImage.rectTransform.sizeDelta = new Vector2(progress, rightImage.rectTransform.sizeDelta.y);
                progress += increment;
                yield return new WaitForSeconds(smoothness);
            } while (progress < 200);
        }
        yield return true;
    }
}
