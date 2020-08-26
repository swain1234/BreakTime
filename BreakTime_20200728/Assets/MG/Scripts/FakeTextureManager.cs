using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FakeTextureManager : MonoBehaviour
{
    [SerializeField] RawImage rawImage; // 카메라에 적용될 이미지
    public RawImage NextPage; // 페이지넘긴 후 씀
    [SerializeField] RenderTexture renderTexture; // 연결해줄 렌더텍스쳐
    private Camera cameraManager;
    private Option option;
    [SerializeField] TextMeshProUGUI stageName;

    void Awake()
    {
        cameraManager = GetComponent<Camera>();
        option = FindObjectOfType<Option>();
    }

    public void TextureCapture_L()
    {
        stageName.text = option.currentLevel.LevelName;
        cameraManager.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraManager.targetTexture = renderTexture;
        cameraManager.Render();

        rawImage.texture = renderTexture;
        RenderTexture.active = renderTexture;
        cameraManager.targetTexture = null;
        RenderTexture.active = null;
    }
}