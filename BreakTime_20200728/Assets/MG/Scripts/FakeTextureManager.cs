using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeTextureManager : MonoBehaviour
{
    private int resWidth = 1600;
    private int resHeight = 1200;

    [SerializeField] RawImage rawImage; // 카메라에 적용될 이미지
    [SerializeField] RenderTexture renderTexture; // 연결해줄 렌더텍스쳐
    private Camera cameraManager;

    void Awake()
    {
        resWidth = Screen.width;
        resHeight = Screen.height;
        cameraManager = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        //if (Input.GetKeyDown("c")) // RightNext에 현재 화면 텍스쳐 적용
        //{
        //    RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        //    cameraManager.targetTexture = rt;
        //    cameraManager.Render();

        //    rawImage.texture = rt;
        //    RenderTexture.active = rt;
        //    cameraManager.targetTexture = null;
        //    RenderTexture.active = null;
        //}
        if (Input.GetKeyDown("x")) // 책에 카메라2화면 보여주기
        {
            cameraManager.targetTexture = renderTexture;
            rawImage.texture = renderTexture;
        }
    }
}