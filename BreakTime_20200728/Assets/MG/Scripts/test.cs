using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class test : MonoBehaviour
{
    [SerializeField] Text dialogueText;
    string source = ""; //읽어낸 텍스트 할당받는 변수
    string i_source = ""; // 이미지용
    string[] buffer; // 임시로 텍스트들을 저장할 배열
    List<string> tArray; // 쉼표로 구분된 대화들을 저장하는 리스트
    List<string> iArray; // 이미지구분을 위한 리스트
    int t_num = 0; // 대화 리스트를 출력할 때 쓸 정수
    int i_num = 0; // 이미지 리스트 출력 정수
    bool isCoroutine = false; // 코루틴 동작여부를 확인하는 bool변수
    string sentence = ""; // 다음문장을 출력할때  쓸 변수
    string i_sentence = ""; // 다음 이미지 출력위한 변수

    [SerializeField] Image currentImage;
    Sprite nextImage;

    private void Start()
    {
        tArray = new List<string>();
        iArray = new List<string>();

    }

    public void ReadDialogue() // 대화 불러오기
    {
        t_num = 0; // 초기화
        StreamReader sr = new StreamReader(Application.dataPath + "/StreamingAssets" + "/" + "test.txt");
        while (source != null)
        {
            buffer = source.Split(',');
            if (buffer.Length == 0)
            {
                sr.Close();
                return;
            }
            for(int i = 0; i < buffer.Length; i++)
            {
                if(buffer[i] != "")
                    tArray.Add(buffer[i]);
            }
            source = sr.ReadLine();    // 한줄 읽는다.
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
            dialogueText.text = sentence;
            isCoroutine = false;
        }
        else
        {
            if (i_num < iArray.Count && t_num < tArray.Count)
            {
                i_sentence = iArray[i_num++];
                nextImage = Resources.Load(i_sentence, typeof(Sprite)) as Sprite;
                Debug.Log(i_sentence);
                currentImage.sprite = nextImage;
                sentence = tArray[t_num++];
                StartCoroutine(TypeSentence(sentence));
            }
        }
    }

    IEnumerator TypeSentence(string sentence) // 하나씩 출력
    {
        isCoroutine = true;

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }

        isCoroutine = false;
    }

    void EndDialogue()
    {
        //animator.SetBool("IsOpen", false);
        Debug.Log("end");
    }

    public void ReadImage() // 이미지 불러오기
    {
        i_num = 0;
        StreamReader sr_image = new StreamReader(Application.dataPath + "/StreamingAssets" + "/" + "test1.txt");
        while (i_source != null)
        {
            buffer = i_source.Split(',');
            if (buffer.Length == 0)
            {
                sr_image.Close();
                return;
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] != "")
                    iArray.Add(buffer[i]);
            }
            i_source = sr_image.ReadLine();    // 한줄 읽는다.
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            ReadImage();
            ReadDialogue();
        }
        if (Input.GetKeyDown("s"))
        {
            DisplayNextSentence();
        }
    }
}
