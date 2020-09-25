using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    static public FadeManager instance;

    public Image black;

    private Color color;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    public void FadeOut(float _speed = 0.02f)
    {
        StartCoroutine(FadeOutCoroutine(_speed));
    }

    IEnumerator FadeOutCoroutine(float _speed)
    {
        black.gameObject.SetActive(true);
        color = black.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            black.color = color;
            yield return waitTime;
        }
    }

    public void FadeIn(float _speed = 0.02f)
    {
        StartCoroutine(FadeInCoroutine(_speed));
    }

    IEnumerator FadeInCoroutine(float _speed)
    {
        color = black.color;

        while (color.a > 0f)
        {
            color.a -= _speed;
            black.color = color;
            yield return waitTime;
        }
        black.gameObject.SetActive(false);
    }
}
