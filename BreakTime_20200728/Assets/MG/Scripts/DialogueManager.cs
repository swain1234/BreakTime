using System.Collections;
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
    Sprite nextImage;

    List<string> tArray; // 쉼표로 구분된 대화들을 저장하는 리스트
    List<string> iArray; // 이미지구분을 위한 리스트
    int t_num = 0; // 대화 리스트를 출력할 때 쓸 정수
    int i_num = 0; // 이미지 리스트 출력 정수
    bool isCoroutine = false; // 코루틴 동작여부를 확인하는 bool변수
    bool isDialogue = false; // 현재 대화여부를 확인하는 bool변수
    bool isStart = true; // 시작대화인지 끝대화인지 확인하는 bool변수
    string sentence = ""; // 다음문장을 출력할때  쓸 변수
    string i_sentence = ""; // 다음 이미지 출력위한 변수
    List<Dictionary<string, object>> data;

    private Option option;
    private wide theWide;

    private void Start()
    {
        tArray = new List<string>();
        iArray = new List<string>();
        option = FindObjectOfType<Option>();
        theWide = FindObjectOfType<wide>();
    }

    public void ReadDialogue(int a) // 대화 및 이미지 불러오기, 0일경우 start 1or다른숫자는 end파일 불러옴
    {
        panel.gameObject.SetActive(true);
        isDialogue = true;
        //위치 초기화
        currentImage.rectTransform.anchoredPosition = new Vector2(-1450f, currentImage.rectTransform.anchoredPosition.y);
        resourceText.rectTransform.anchoredPosition = new Vector2(346.6f, resourceText.rectTransform.anchoredPosition.y);
        nameTag.rectTransform.anchoredPosition = new Vector2(-600f, nameTag.rectTransform.anchoredPosition.y);
        t_num = 0; // 초기화
        i_num = 0;

        tArray = new List<string>();
        iArray = new List<string>();

        if(a == 0)
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
        }

        DisplayNextSentence();
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

                if (i_sentence == "librarian")
                {
                    if (i_num >= 2 && i_sentence != iArray[i_num - 2]) // 다른사람에서 사서로 사람이 바뀌면 위치반대(이미 다른사람이 위치를 오른쪽으로 이동시켰기때문)
                    {
                        currentImage.rectTransform.anchoredPosition = new Vector2(-currentImage.rectTransform.anchoredPosition.x, currentImage.rectTransform.anchoredPosition.y);
                        resourceText.rectTransform.anchoredPosition = new Vector2(-resourceText.rectTransform.anchoredPosition.x, resourceText.rectTransform.anchoredPosition.y);
                        nameTag.rectTransform.anchoredPosition = new Vector2(-nameTag.rectTransform.anchoredPosition.x, nameTag.rectTransform.anchoredPosition.y);
                    }
                }
                else
                {
                    if (i_num == 1 || (i_num >= 2 && "librarian" == iArray[i_num - 2])) // 사서가아닌인물의 처음대화 또는 이전인물이 사서일경우 위치반대
                    {
                        currentImage.rectTransform.anchoredPosition = new Vector2(-currentImage.rectTransform.anchoredPosition.x, currentImage.rectTransform.anchoredPosition.y);
                        resourceText.rectTransform.anchoredPosition = new Vector2(-resourceText.rectTransform.anchoredPosition.x, resourceText.rectTransform.anchoredPosition.y);
                        nameTag.rectTransform.anchoredPosition = new Vector2(-nameTag.rectTransform.anchoredPosition.x, nameTag.rectTransform.anchoredPosition.y);
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
            //레벨 클리어 시 책 연출 이동
            Debug.Log("레벨클리어, 연출이동");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            ReadDialogue(0);
        }
        if (Input.GetKeyDown("s"))
        {
            ReadDialogue(1);
        }
        if (isDialogue == true && Input.anyKeyDown)
        {
            DisplayNextSentence();
        }
    }
}
