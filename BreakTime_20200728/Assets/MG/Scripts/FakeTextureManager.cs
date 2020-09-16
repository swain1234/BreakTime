using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FakeTextureManager : MonoBehaviour // 책 좌측
{
    public RawImage rawImage; // 카메라에 적용될 이미지
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

    public void TextureLeft()
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

    public void TextureRight()
    {
        cameraManager.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraManager.targetTexture = renderTexture;
        cameraManager.Render();

        rawImage.texture = renderTexture;
        RenderTexture.active = renderTexture;
        cameraManager.targetTexture = null;
        RenderTexture.active = null;
    }

    public void TextureNext()
    {
        stageName.text = option.nextLevel.LevelName;
        cameraManager.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraManager.targetTexture = renderTexture;
        cameraManager.Render();

        NextPage.texture = renderTexture;
        RenderTexture.active = renderTexture;
        cameraManager.targetTexture = null;
        RenderTexture.active = null;
    }
}