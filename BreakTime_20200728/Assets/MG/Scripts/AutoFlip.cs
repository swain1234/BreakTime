using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

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

    private FakeTextureManager3 fakeTextureManager3;
    private FakeTextureManager fakeTextureManager;
    private TextureManager textureManager;
    private FadeManager fadeManager;
    private Option option;
    [SerializeField] TextMeshProUGUI tmi;
    [SerializeField] TextMeshProUGUI bonusText;
    [SerializeField] Image retry;
    [SerializeField] Image selectLevel;
    [SerializeField] Image nextLevel;
    [SerializeField] Image clear;

    List<string> tArray; // 쉼표로 구분된 대화들을 저장하는 리스트
    List<string> bArray; // 쉼표로 구분된 대화들을 저장하는 리스트
    int t_num = 0; // 대화 리스트를 출력할 때 쓸 정수
    int b_num = 0; // 대화 리스트를 출력할 때 쓸 정수
    string sentence = ""; // 다음문장을 출력할때  쓸 변수
    string bSentence = "";

    // Use this for initialization
    void Start () {
        if (!ControledBook)
            ControledBook = GetComponent<Book>();
        if (AutoStartFlip)
            StartFlipping();
        ControledBook.OnFlip.AddListener(new UnityEngine.Events.UnityAction(PageFlipped));
        textureManager = FindObjectOfType<TextureManager>();
        fakeTextureManager = FindObjectOfType<FakeTextureManager>();
        fakeTextureManager3 = FindObjectOfType<FakeTextureManager3>();
        fadeManager = FindObjectOfType<FadeManager>();
        option = FindObjectOfType<Option>();

        string[] num = option.currentLevel.LevelName.Split('_');
        b_num = int.Parse(num[0]);
        tArray = new List<string>();
        bArray = new List<string>();
        List<Dictionary<string, object>> Tdata = CSVReader.Read("01_A_Sweetie_in_Red_End");
        List<Dictionary<string, object>> Bdata = CSVReader.Read("01_A_Sweetie_in_Red_End");

        for (var i = 0; i < Tdata.Count; i++)
        {
            tArray.Add((string)Tdata[i]["script"]);
        }
        for (var i = 0; i < Bdata.Count; i++)
        {
            bArray.Add((string)Bdata[i]["script"]);
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

    public void PrintBonusText()
    {
        StartCoroutine(BonusTextPrint());
    }

    IEnumerator BonusTextPrint()
    {
        bSentence = bArray[b_num - 1];
        bonusText.text = "";
        foreach (char letter in bSentence.ToCharArray())
        {
            bonusText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3f);
        for (int k = bSentence.Length; k > 0; k--)
        {
            bonusText.text = bSentence.Substring(0, k);
            yield return new WaitForSeconds(0.04f);
        }
        bonusText.text = "";
    }

    public void ImageOn()
    {
        retry.gameObject.SetActive(true);
        selectLevel.gameObject.SetActive(true);
        nextLevel.gameObject.SetActive(true);
        clear.gameObject.SetActive(true);
    }

    public void ImageOff()
    {
        retry.gameObject.SetActive(false);
        selectLevel.gameObject.SetActive(false);
        nextLevel.gameObject.SetActive(false);
        clear.gameObject.SetActive(false);
    }

    public void Retry()
    {
        option.Retry();
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
        bonusText.text = "";
        StartCoroutine(LevelNext());
    }

    IEnumerator LevelNext()
    {
        FlipRightPage();
        fakeTextureManager3.rawImage.texture = ConvertSpriteToTexture(option.nextLevel.Icon);
        fakeTextureManager3.TextureCapture_R();
        yield return new WaitForSeconds(2f);
        sentence = tArray[t_num];
        tmi.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            tmi.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(5f);
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        b_num++; // 다음레벨의 보너스문장
        tmi.text = "";
        option.LevelChange();
        option.StageScript();
        // 다음레벨 위치로 이동
        this.transform.GetChild(0).gameObject.SetActive(false);
        fadeManager.FadeIn();
        yield return new WaitForSeconds(0.5f);
    }
}
