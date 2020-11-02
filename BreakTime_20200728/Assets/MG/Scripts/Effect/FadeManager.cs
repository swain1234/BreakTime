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
    private WaitForSeconds wait = new WaitForSeconds(0.7f);

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

    public static FadeManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void Fade(float _speed = 0.02f)
    {
        StartCoroutine(FadeCoroutine(_speed));
    }

    IEnumerator FadeCoroutine(float _speed)
    {
        black.gameObject.SetActive(true);
        color = black.color;
        while (color.a < 1f)
        {
            color.a += _speed;
            black.color = color;
            yield return waitTime;
        }
        yield return wait;
        while (color.a > 0f)
        {
            color.a -= _speed;
            black.color = color;
            yield return waitTime;
        }
        black.gameObject.SetActive(false);
    }
}
