using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letterbox : MonoBehaviour
{
    static public Letterbox instance;

    [SerializeField] Image upImage;
    [SerializeField] Image downImage;
    [SerializeField] Image leftImage;
    [SerializeField] Image rightImage;

    float smoothness = 0.005f;
    float duration = 0.001f;

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

    public static Letterbox Instance
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

    void Start()
    {
        upImage = upImage.GetComponent<Image>();
        downImage = downImage.GetComponent<Image>();
        leftImage = leftImage.GetComponent<Image>();
        rightImage = rightImage.GetComponent<Image>();
    }

    public void initSetting()
    {
        upImage.rectTransform.sizeDelta = new Vector2(upImage.rectTransform.sizeDelta.x, 0);
        downImage.rectTransform.sizeDelta = new Vector2(downImage.rectTransform.sizeDelta.x, 0);
        leftImage.rectTransform.sizeDelta = new Vector2(0, leftImage.rectTransform.sizeDelta.y);
        rightImage.rectTransform.sizeDelta = new Vector2(0, rightImage.rectTransform.sizeDelta.y);
        upImage.gameObject.SetActive(false);
        downImage.gameObject.SetActive(false);
        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);
    }

    public void WideMode()
    {
        StartCoroutine(WideChange());
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
            } while (progress >= -100);
            upImage.gameObject.SetActive(false);
            downImage.gameObject.SetActive(false);
        }
        else
        {
            upImage.gameObject.SetActive(true);
            downImage.gameObject.SetActive(true);
            do
            {
                upImage.rectTransform.sizeDelta = new Vector2(upImage.rectTransform.sizeDelta.x, progress);
                downImage.rectTransform.sizeDelta = new Vector2(downImage.rectTransform.sizeDelta.x, progress);
                progress += increment;
                yield return new WaitForSeconds(smoothness);
            } while (progress <= 400);
        }
        yield return true;
    }

    public void NarrowMode()
    {
        StartCoroutine(NarrowChange());
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
            } while (progress >= -100);
            leftImage.gameObject.SetActive(false);
            rightImage.gameObject.SetActive(false);
        }
        else
        {
            leftImage.gameObject.SetActive(true);
            rightImage.gameObject.SetActive(true);
            do
            {
                leftImage.rectTransform.sizeDelta = new Vector2(progress, leftImage.rectTransform.sizeDelta.y);
                rightImage.rectTransform.sizeDelta = new Vector2(progress, rightImage.rectTransform.sizeDelta.y);
                progress += increment;
                yield return new WaitForSeconds(smoothness);
            } while (progress <= 400);
        }
        yield return true;
    }

    private void Update()
    {
        //if (Input.GetKeyDown("a"))
        //{
        //    WideMode();
        //}
        //if (Input.GetKeyDown("s"))
        //{
        //    NarrowMode();
        //}
    }
}
