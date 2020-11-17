using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolingPaint : MonoBehaviour
{
    static public PoolingPaint instance;

    private ObjectPoolClass objectPoolClass;
    public GameObject paintObjectPool;
    int i, j = 0;
    string s = "";

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        objectPoolClass = GetComponent<ObjectPoolClass>();
    }

    public static PoolingPaint Instance
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

    public void SplashPaint()
    {
        StartCoroutine(ISplash());
    }

    private IEnumerator ISplash()
    {
        for (i = 0; i < Random.Range(3,6); i++)
        {
            Image paint = objectPoolClass.GetObject();
            j = Random.Range(0, 3);
            if (j == 0)
                s = "r";
            else if (j == 1)
                s = "g";
            else
                s = "b";
            paint.sprite = Resources.Load(s + Random.Range(1, 4), typeof(Sprite)) as Sprite;
            // 오브젝트를 받아와서
            if (paint == null) yield return null;
            // 없다면 종료
            AudioManager.Instance.Play("brush");
            paint.rectTransform.anchoredPosition = new Vector2(Random.Range(0, 385) * 10, Random.Range(0, 217) * 10);
            paint.rectTransform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            paint.GetComponent<PaintBehaviour>().Spawn();
            // 있으면 활성화
            yield return new WaitForSeconds(0.2f);
        }
    }
}