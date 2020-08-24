using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resourceText;
    [SerializeField] Image currentImage;
    Sprite nextImage;

    List<string> tArray; // 쉼표로 구분된 대화들을 저장하는 리스트
    List<string> iArray; // 이미지구분을 위한 리스트
    int t_num = 0; // 대화 리스트를 출력할 때 쓸 정수
    int i_num = 0; // 이미지 리스트 출력 정수
    bool isCoroutine = false; // 코루틴 동작여부를 확인하는 bool변수
    string sentence = ""; // 다음문장을 출력할때  쓸 변수
    string i_sentence = ""; // 다음 이미지 출력위한 변수

    private void Start()
    {
        tArray = new List<string>();
        iArray = new List<string>();
    }

    public void ReadDialogue() // 대화 및 이미지 불러오기
    {
        t_num = 0; // 초기화
        i_num = 0;

        tArray = new List<string>();
        iArray = new List<string>();

        List<Dictionary<string, object>> data = CSVReader.Read("A_Sweetie_in_Red_Start");

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
                nextImage = Resources.Load(i_sentence, typeof(Sprite)) as Sprite;
                currentImage.sprite = nextImage;
                sentence = tArray[t_num++];

                //currentImage.rectTransform.



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
        //창닫고 wide연출시작
        //레벨클리어시 다시 화면원래대로돌리고 대화연출 후 책넘기는연출
    }

    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            ReadDialogue();
        }
        if (Input.GetKeyDown("s"))
        {
            DisplayNextSentence();
        }
    }
}
