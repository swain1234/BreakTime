﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resourceText;
    [SerializeField] Image currentImage;
    [SerializeField] Image nameTag;
    [SerializeField] Image panel;
    [SerializeField] AutoFlip autoFlip;
    [SerializeField] StopManager stopManager;
    Sprite nextImage;

    List<string> tArray; // 쉼표로 구분된 대화들을 저장하는 리스트
    List<string> iArray; // 이미지구분을 위한 리스트
    List<string> pArray; // 포지션확인을 위한 리스트
    int t_num = 0; // 대화 리스트를 출력할 때 쓸 정수
    int i_num = 0; // 이미지 리스트 출력 정수
    int p_num = 0;
    public bool isCoroutine = false; // 코루틴 동작여부를 확인하는 bool변수
    public bool isDialogue = false; // 현재 대화여부를 확인하는 bool변수
    bool isStart = true; // 시작대화인지 끝대화인지 확인하는 bool변수
    string sentence = ""; // 다음문장을 출력할때  쓸 변수
    string i_sentence = ""; // 다음 이미지 출력위한 변수
    string p_sentence = "";
    List<Dictionary<string, object>> data;

    private Option option;
    private wide theWide;
    private FadeManager fadeManager;
    [SerializeField] FakeTextureManager fakeTextureManager;
    [SerializeField] FakeTextureManager_R fakeTextureManager_R;
    [SerializeField] TextureManager textureManager;

    private void Start()
    {
        tArray = new List<string>();
        iArray = new List<string>();
        pArray = new List<string>();
        option = FindObjectOfType<Option>();
        theWide = FindObjectOfType<wide>();
        fadeManager = FindObjectOfType<FadeManager>();
    }

    public void ReadDialogue(int a) // 대화 및 이미지 불러오기, 0일경우 start 1or다른숫자는 end파일 불러옴
    {
        if(fadeManager.black.color.a != 0)
        {
            //Color c = fadeManager.black.color;
            //c.a = 0;
            //fadeManager.black.color = c;
            //fadeManager.black.gameObject.SetActive(false);
            fadeManager.FadeIn();
        }
        panel.gameObject.SetActive(true);
        isDialogue = true;
        //위치 초기화
        currentImage.rectTransform.anchoredPosition = new Vector2(-1450f, currentImage.rectTransform.anchoredPosition.y);
        resourceText.rectTransform.anchoredPosition = new Vector2(346.6f, resourceText.rectTransform.anchoredPosition.y);
        nameTag.rectTransform.anchoredPosition = new Vector2(-600f, nameTag.rectTransform.anchoredPosition.y);
        if (currentImage.transform.localScale.x < 0)
            Flip();
        t_num = 0; // 초기화
        i_num = 0;
        p_num = 0;

        tArray = new List<string>();
        iArray = new List<string>();
        pArray = new List<string>();

        if (a == 0)
        {
            data = CSVReader.Read(option.currentLevel.LevelName + "_Start");
            isStart = true;
        }
        else
        {
            data = CSVReader.Read(option.currentLevel.LevelName + "_End");
            isStart = false;
        }


        for (var i = 0; i < data.Count; i++)
        {
            tArray.Add((string)data[i]["script"]);
            iArray.Add((string)data[i]["character"]);
            pArray.Add((string)data[i]["position"]);
        }

        DisplayNextSentence();
    }

    private void Flip()
    {
        Vector3 scale = currentImage.transform.localScale;
        if (currentImage.transform.localScale.x > 0)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);
        currentImage.transform.localScale = scale;
    }
    private void PositionFlip()
    {
        currentImage.rectTransform.anchoredPosition = new Vector2(-currentImage.rectTransform.anchoredPosition.x, currentImage.rectTransform.anchoredPosition.y);
        resourceText.rectTransform.anchoredPosition = new Vector2(-resourceText.rectTransform.anchoredPosition.x, resourceText.rectTransform.anchoredPosition.y);
        nameTag.rectTransform.anchoredPosition = new Vector2(-nameTag.rectTransform.anchoredPosition.x, nameTag.rectTransform.anchoredPosition.y);
    }

    public void DisplayNextSentence() // 다음 문장과 이미지
    {
        if (t_num == tArray.Count)
        {
            EndDialogue();
            return;
        }
        if (isCoroutine) // 다른 코루틴이랑 겹칠수도있음 조심
        {
            StopAllCoroutines();
            resourceText.text = sentence;
            isCoroutine = false;
        }
        else
        {
            if (i_num < iArray.Count && t_num < tArray.Count)
            {
                i_sentence = iArray[i_num++];
                nameTag.sprite = Resources.Load(i_sentence + "Tag", typeof(Sprite)) as Sprite; // 태그넣기
                p_sentence = pArray[p_num++];

                if(i_sentence != "librarian") // 캐릭터이미지 스케일을보고 flip하기, 이미지position을 보고 positionflip하기
                {
                    if (p_sentence == "l")
                    {
                        if (currentImage.transform.localScale.x > 0)
                        {
                            Flip();
                        }
                        if (currentImage.rectTransform.anchoredPosition.x > 0)
                        {
                            PositionFlip();
                        }
                    }
                    else
                    {
                        if (currentImage.transform.localScale.x < 0)
                        {
                            Flip();
                        }
                        if (currentImage.rectTransform.anchoredPosition.x < 0)
                        {
                            PositionFlip();
                        }
                    }
                }
                else
                {
                    if (p_sentence == "l")
                    {
                        if (currentImage.transform.localScale.x < 0)
                        {
                            Flip();
                        }
                        if (currentImage.rectTransform.anchoredPosition.x > 0)
                        {
                            PositionFlip();
                        }
                    }
                    else
                    {
                        if (currentImage.transform.localScale.x > 0)
                        {
                            Flip();
                        }
                        if (currentImage.rectTransform.anchoredPosition.x < 0)
                        {
                            PositionFlip();
                        }
                    }
                }

                nextImage = Resources.Load(i_sentence, typeof(Sprite)) as Sprite;
                currentImage.sprite = nextImage;
                sentence = tArray[t_num++];
                sentence = sentence.Replace("&", "\n"); // &문자를 개행문자로 변경
                StartCoroutine(TypeSentence(sentence));
            }
        }
    }

    IEnumerator TypeSentence(string sentence) // 글자 하나씩 출력하는 코루틴
    {
        isCoroutine = true;

        resourceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            resourceText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
        isCoroutine = false;
    }

    void EndDialogue() // 대화가 끝났을때 연출
    {
        isDialogue = false;
        panel.gameObject.SetActive(false);
        if (option.currentLevel.LevelName == "08_Grand_Fall" || option.currentLevel.LevelName == "07_Honey_Bunny_Hop")
        {
            theWide.NarrowMode();
        }
        else
        {
            theWide.WideMode();
        }
        if (isStart == false)
        {
            StartCoroutine(DialogueEnd());
        }
        else
        {
            stopManager.ScriptON();
        }
    }

    IEnumerator DialogueEnd()
    {
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        autoFlip.ImageOn();
        autoFlip.transform.GetChild(0).gameObject.SetActive(true);
        textureManager.TextureCapture();
        fakeTextureManager.TextureCapture_L();
        fakeTextureManager_R.TextureCapture();
        fadeManager.FadeIn();
        //yield return new WaitForSeconds(1f);
        autoFlip.PrintBonusText();
    }

    private void Update()
    {
        //if (Input.GetKeyDown("a"))
        //{
        //    ReadDialogue(0);
        //}
        //if (Input.GetKeyDown("s"))
        //{
        //    ReadDialogue(1);
        //}
        if (isDialogue == true && Input.anyKeyDown)
        {
            DisplayNextSentence();
        }
    }
}
