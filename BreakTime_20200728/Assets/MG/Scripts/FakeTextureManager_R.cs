using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeTextureManager_R : MonoBehaviour
{
    [SerializeField] RawImage rawImage_R; // 카메라에 적용될 이미지
    [SerializeField] RenderTexture renderTexture_R; // 연결해줄 렌더텍스쳐
    private Camera cameraManager;

    void Awake()
    {
        cameraManager = GetComponent<Camera>();
    }


    public void TextureCapture()
    {
        cameraManager.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraManager.targetTexture = renderTexture_R;
        cameraManager.Render();

        rawImage_R.texture = renderTexture_R;
        RenderTexture.active = renderTexture_R;
        cameraManager.targetTexture = null;
        RenderTexture.active = null;
    }
}