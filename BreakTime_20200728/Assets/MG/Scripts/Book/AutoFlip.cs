﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Book))]
public class AutoFlip : MonoBehaviour {
    public FlipMode Mode;
    public float PageFlipTime = 1;
    public float TimeBetweenPages = 1;
    public float DelayBeforeStarting = 0;
    public bool AutoStartFlip=true;
    public Book ControledBook;
    public int AnimationFramesCount = 40;
    bool isFlipping = false;

    private TextureManager textureManager;
    private Option option;
    private GameManager gameManager;
    [SerializeField] TextMeshProUGUI tmi;
    [SerializeField] Image retry;
    [SerializeField] Image selectLevel;
    [SerializeField] Image nextLevel;
    [SerializeField] Image body;
    [SerializeField] Image clear;
    [SerializeField] Image fakeClear;
    [SerializeField] FakeTextureManager bookLeft;
    [SerializeField] FakeTextureManager bookRight;
    [SerializeField] FakeTextureManager bookNext;

    List<string> tArray; // 쉼표로 구분된 대화들을 저장하는 리스트
    List<string> bArray; // 쉼표로 구분된 대화들을 저장하는 리스트
    int t_num = 0; // 대화 리스트를 출력할 때 쓸 정수
    string sentence = ""; // 다음문장을 출력할때  쓸 변수
    List<Dictionary<string, object>> Tdata;

    // Use this for initialization
    void Start () {
        if (!ControledBook)
            ControledBook = GetComponent<Book>();
        if (AutoStartFlip)
            StartFlipping();
        ControledBook.OnFlip.AddListener(new UnityEngine.Events.UnityAction(PageFlipped));
        textureManager = FindObjectOfType<TextureManager>();
        option = FindObjectOfType<Option>();
        gameManager = FindObjectOfType<GameManager>();
        tArray = new List<string>();
        bArray = new List<string>();
        List<Dictionary<string, object>> data = CSVReader.Read("Clear");
        for (var i = 0; i < data.Count; i++)
        {
            tArray.Add((string)data[i]["end"]);
        }
    }

    void PageFlipped()
    {
        isFlipping = false;
    }
	public void StartFlipping()
    {
        StartCoroutine(FlipToEnd());
    }
    public void FlipRightPage()
    {
        //여기에 책넘긴이후 일어날 모든 변경사항잇어야할듯
        //코루틴으로 넣어서 옵션의 레벨데이터등 변경
        if (isFlipping) return;
        if (ControledBook.currentPage >= ControledBook.TotalPageCount) return;
        isFlipping = true;
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.9f;
        float dx = (xl)*2 / AnimationFramesCount;
        StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
    }
    IEnumerator FlipToEnd()
    {
        yield return new WaitForSeconds(DelayBeforeStarting);
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2)*0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y)*0.9f;
        //y=-(h/(xl)^2)*(x-xc)^2          
        //               y         
        //               |          
        //               |          
        //               |          
        //_______________|_________________x         
        //              o|o             |
        //           o   |   o          |
        //         o     |     o        | h
        //        o      |      o       |
        //       o------xc-------o      -
        //               |<--xl-->
        //               |
        //               |
        float dx = (xl)*2 / AnimationFramesCount;
        switch (Mode)
        {
            case FlipMode.RightToLeft:
                while (ControledBook.currentPage < ControledBook.TotalPageCount)
                {
                    StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(TimeBetweenPages);
                }
                break;
        }
    }
    IEnumerator FlipRTL(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc + xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);

        ControledBook.DragRightPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < AnimationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            ControledBook.UpdateBookRTLToPoint(new Vector3(x, y, 0));
            yield return new WaitForSeconds(frameTime);
            x -= dx;
        }
        ControledBook.ReleasePage();
    }

    Texture2D ConvertSpriteToTexture(Sprite sprite)
    {
        try
        {
            if (sprite.rect.width != sprite.texture.width)
            {
                Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                Color[] colors = newText.GetPixels();
                Color[] newColors = sprite.texture.GetPixels((int)System.Math.Ceiling(sprite.textureRect.x),
                                                             (int)System.Math.Ceiling(sprite.textureRect.y),
                                                             (int)System.Math.Ceiling(sprite.textureRect.width),
                                                             (int)System.Math.Ceiling(sprite.textureRect.height));
                Debug.Log(colors.Length + "_" + newColors.Length);
                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            }
            else
                return sprite.texture;
        }
        catch
        {
            return sprite.texture;
        }
    }

    public void LevelClear()
    {
        StartCoroutine(BookEffect());
    }

    IEnumerator BookEffect()
    {
        yield return new WaitForSeconds(1f);
        if (option.isCandy == true)
        {
            clear.sprite = Resources.Load("clearCandy", typeof(Sprite)) as Sprite;
            fakeClear.sprite = Resources.Load("clearCandy", typeof(Sprite)) as Sprite;
        }
        else
        {
            clear.sprite = Resources.Load("clear", typeof(Sprite)) as Sprite;
            fakeClear.sprite = Resources.Load("clear", typeof(Sprite)) as Sprite;
        }
        clear.gameObject.SetActive(true);
        ImageOn();
    }

    public void ImageOn()
    {
        body.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);
        selectLevel.gameObject.SetActive(true);
        nextLevel.gameObject.SetActive(true);
    }

    public void ImageOff()
    {
        body.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        selectLevel.gameObject.SetActive(false);
        nextLevel.gameObject.SetActive(false);
    }

    public void Retry()
    {
        option.Retry();
        ImageOff();
        clear.gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void LevelSelect()
    {
        option.LevelSelect();
    }

    public void NextLevel()
    {
        ImageOff();
        StopAllCoroutines();
        fakeClear.gameObject.SetActive(true);
        bookRight.TextureRight();
        clear.gameObject.SetActive(false);
        fakeClear.gameObject.SetActive(false);
        option.isCandy = false;
        StartCoroutine(LevelNext());
    }

    IEnumerator LevelNext()
    {
        if (option.nextLevel != null)
        {
            FlipRightPage();
            bookNext.rawImage.texture = ConvertSpriteToTexture(option.nextLevel.Icon);
            bookNext.TextureNext();
            yield return new WaitForSeconds(2f);
            if (tArray[t_num] != null)
                sentence = tArray[t_num++];
            else
                sentence = "아직 안만들어졌어요";
            sentence = sentence.Replace("%", ",");
            sentence = sentence.Replace(". ", ".\n");
            tmi.text = "";
            foreach (char letter in sentence.ToCharArray()) // 보너스문장 한글자씩
            {
                tmi.text += letter;
                if (letter.Equals('.') == true)
                    yield return new WaitForSeconds(0.2f);
                else
                    yield return new WaitForSeconds(0.03f);
            }
            yield return new WaitForSeconds(1f);
            FadeManager.Instance.Fade();
            yield return new WaitForSeconds(1f);
            tmi.text = "";
            option.LevelChange();
            option.StageScript();
            gameManager.NextStage();
            gameManager.StartPosition();
            this.transform.GetChild(0).gameObject.SetActive(false);
            SceneManager.LoadScene("Stage");
            yield return new WaitForSeconds(0.5f);
        }
        else // 마지막레벨을 클리어했을때
        {
            FadeManager.Instance.Fade();
            yield return new WaitForSeconds(1f);
            //클리어했을떄의 무언가 연출 후 끝내기
            this.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
