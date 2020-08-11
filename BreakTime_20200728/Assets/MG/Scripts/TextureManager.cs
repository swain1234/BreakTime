using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureManager : MonoBehaviour
{
    private int resWidth = 1600;
    private int resHeight = 1200;

    [SerializeField] RawImage rawImage;

    void Awake()
    {
        resWidth = Screen.width;
        resHeight = Screen.height;
    }

    void LateUpdate()
    {
        if(Input.GetKeyDown("k"))
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            GetComponent<Camera>().targetTexture = rt;
            GetComponent<Camera>().Render();
            rawImage.texture = rt;
            RenderTexture.active = rt;
            GetComponent<Camera>().targetTexture = null;
            RenderTexture.active = null;
        }
    }
}
