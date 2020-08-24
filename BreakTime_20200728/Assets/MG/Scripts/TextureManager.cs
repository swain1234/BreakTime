using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureManager : MonoBehaviour
{
    private int resWidth;
    private int resHeight;

    [SerializeField] RawImage fakeImage;
    private Camera cameraManager;

    void Awake()
    {
        resWidth = Screen.width;
        resHeight = Screen.height;
        cameraManager = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if(Input.GetKeyDown("z")) // 클리어 후 결과 화면 카메라2 삽화처럼 삽입
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
}
