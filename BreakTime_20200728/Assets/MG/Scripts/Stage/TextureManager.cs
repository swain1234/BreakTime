using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureManager : MonoBehaviour
{
    private int resWidth;
    private int resHeight;

    public RawImage fakeImage;
    private Camera cameraManager;

    void Awake()
    {
        resWidth = Screen.width;
        resHeight = Screen.height;
        cameraManager = GetComponent<Camera>();
    }

    public void TextureCapture()
    {
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        cameraManager.targetTexture = rt;
        cameraManager.Render();

        fakeImage.texture = rt;
        RenderTexture.active = rt;
        cameraManager.targetTexture = null;
        RenderTexture.active = null;
    }
}
