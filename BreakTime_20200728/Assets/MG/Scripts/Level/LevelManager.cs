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
    public int num = 0; // 자식 번호
    private float time = 0; // 깜박이는 효과를 위한 변수
    private GameObject child; // 선택하는 자식오브젝트
    private SceneChanage sceneChange;
    private Option option;
    private Title title;
    string backgroundMusic = "LevelScene";
    bool isSelect = false;

    [SerializeField] TextMeshProUGUI resourceText;
    [SerializeField] GameObject choice;

    void Start()
    {
        sceneChange = GetComponentInParent<SceneChanage>();
        child = transform.GetChild(num).gameObject;
        resourceText.text = child.GetComponent<LevelParent>().levelData.Script;
        sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
        option = FindObjectOfType<Option>();
        title = FindObjectOfType<Title>();
        StartCoroutine(SelectEffect());
        AudioManager.Instance.FadeIn(backgroundMusic);
        isSelect = false;
    }

    void Update()
    {
        levelSelect();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RectTransform rect = choice.GetComponent<RectTransform>();
            if (rect.offsetMin.x != 100)
                LevelChoice();
            else
                LevelSceneChange();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            Close();
    }

    private void levelSelect() // 레벨선택하는 함수
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextButton();
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousButton();
        }
    }

    public void NextButton()
    {
        child.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        num++;
        if (num >= transform.childCount)
            num = 0;
        child = transform.GetChild(num).gameObject;

        resourceText.text = child.GetComponent<LevelParent>().levelData.Script;
        sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
    }

    public void PreviousButton()
    {
        child.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        num--;
        if (num < 0)
            num = transform.childCount - 1;
        child = transform.GetChild(num).gameObject;

        resourceText.text = child.GetComponent<LevelParent>().levelData.Script;
        sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
    }

    public void Close()
    {
        RectTransform rect = choice.GetComponent<RectTransform>();
        rect.offsetMin = new Vector2(5000, rect.offsetMin.y); //Left 5000
        rect.offsetMax = new Vector2(5000, rect.offsetMax.y); //Right -5000
    }

    IEnumerator SelectEffect() // 현재 선택중인 오브젝트에 효과부여
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        while (true)
        {
            if (time <= 0.3f)
                child.GetComponent<Image>().color = new Color(1 - time, 1 - time, 1 - time, 1);
            else
            {
                child.GetComponent<Image>().color = new Color(0.4f + time, 0.4f + time, 0.4f + time, 1);
                if (time >= 0.6f)
                    time = 0;
            }
            time += Time.deltaTime;
            yield return wait;
        }
    }

    public void LevelSceneChange()
    {
        if (!isSelect)
        {
            isSelect = true;
            option.currentLevel = child.GetComponent<LevelParent>().levelData;
            AudioManager.Instance.FadeOut(backgroundMusic);
            option.StageScript();
            LevelLoader.Instance.LoadLevel("Stage");
        }
    }

    public void LevelChoice()
    {
        StartCoroutine(ChoiceLevel());
    }

    IEnumerator ChoiceLevel()
    {
        yield return new WaitForSeconds(0.001f);
        child = transform.GetChild(num).gameObject;
        resourceText.text = child.GetComponent<LevelParent>().levelData.Script;
        sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;

        RectTransform rect = choice.GetComponent<RectTransform>();
        rect.offsetMin = new Vector2(100, rect.offsetMin.y); //Left
        rect.offsetMax = new Vector2(-100, rect.offsetMax.y); //Right
    }
}
