using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Image sampleImage; // 가져오는 이미지
    private int num = 0; // 자식 번호
    private float time = 0; // 깜박이는 효과를 위한 변수
    private GameObject child; // 선택하는 자식오브젝트
    private SceneChanage sceneChange;
    private FadeManager fadeManager;
    private Option option;
    private Title title;
    [SerializeField] TextMeshProUGUI resourceText;

    void Start()
    {
        sceneChange = GetComponentInParent<SceneChanage>();
        child = transform.GetChild(num).gameObject;
        resourceText.text = child.GetComponent<LevelParent>().levelData.Script;
        sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
        fadeManager = FindObjectOfType<FadeManager>();
        option = FindObjectOfType<Option>();
        title = FindObjectOfType<Title>();
        StartCoroutine(SelectEffect());
    }

    void Update()
    {
        levelSelect();
        if (Input.GetKeyDown(KeyCode.Space))
            LevelSceneChange();
    }


    private void levelSelect() // 레벨선택하는 함수
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            child.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            num++;
            if (num >= transform.childCount - 1)
                num = 0;
            child = transform.GetChild(num).gameObject;

            resourceText.text = child.GetComponent<LevelParent>().levelData.Script;
            sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            child.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            num--;
            if (num < 0)
                num = transform.childCount - 2;
            child = transform.GetChild(num).gameObject;

            resourceText.text = child.GetComponent<LevelParent>().levelData.Script;
            sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
        }
    }

    IEnumerator SelectEffect() // 현재 선택중인 오브젝트에 효과부여
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        while (true)
        {
            if (time < 0.5f)
                child.GetComponent<Image>().color = new Color(1, 1, 1, 1 - time);
            else
            {
                child.GetComponent<Image>().color = new Color(1, 1, 1, time);
                if (time > 1f)
                    time = 0;
            }
            time += Time.deltaTime;
            yield return wait;
        }
    }

    public void LevelSceneChange()
    {
        StartCoroutine(SceneTransfer());
    }

    IEnumerator SceneTransfer()
    {
        //책 효과
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Stage");
        string a = child.GetComponent<LevelParent>().levelData.LevelName;
        option.currentLevel = child.GetComponent<LevelParent>().levelData;
        option.StageScript();
        switch (a)
        {
            case "01_A_Sweetie_in_Red":
                //좌표넣기 , 초기셋팅상태
                Debug.Log(a);
                break;
            case "02_Fine_Painting":
                break;
            case "03_Big_Colored_Button":
                break;
            case "04_Pain_Painting":
                break;
            case "05_Black_Purr":
                break;
            case "06_This_War_of_Us":
                break;
            case "07_Honey_Bunny_Hop":
                break;
            case "08_Grand_Fall":
                break;
            case "09_Vanilla_Sky":
                break;
            case "10_Black_Howling":
                break;
        }
        fadeManager.FadeIn();
        yield return new WaitForSeconds(0.5f);

    }

    public void LevelClick()
    {
        StartCoroutine(ClickTransfer());
    }
    
    IEnumerator ClickTransfer()
    {
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Stage");
        string a = child.GetComponent<LevelParent>().levelData.LevelName;
        option.currentLevel = child.GetComponent<LevelParent>().levelData;
        option.StageScript();
        switch (a)
        {
            case "01_A_Sweetie_in_Red":
                //좌표넣기 , 초기셋팅상태
                Debug.Log(a);
                break;
            case "02_Fine_Painting":
                break;
            case "03_Big_Colored_Button":
                break;
            case "04_Pain_Painting":
                break;
            case "05_Black_Purr":
                break;
            case "06_This_War_of_Us":
                break;
            case "07_Honey_Bunny_Hop":
                break;
            case "08_Grand_Fall":
                break;
            case "09_Vanilla_Sky":
                break;
            case "10_Black_Howling":
                break;
        }
        fadeManager.FadeIn();
        yield return new WaitForSeconds(0.5f);
    }
}
