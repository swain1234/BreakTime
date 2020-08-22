using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Text childScript; // 가져오는 텍스트
    [SerializeField]
    private Image sampleImage; // 가져오는 이미지
    private int num = 0; // 자식 번호
    private float time = 0; // 깜박이는 효과를 위한 변수
    private GameObject child; // 선택하는 자식오브젝트
    private SceneChanage sceneChange;
    private FadeManager fadeManager;
    private Title title;

    void Start()
    {
        sceneChange = GetComponentInParent<SceneChanage>();
        child = transform.GetChild(num).gameObject;
        childScript.text = child.GetComponent<LevelParent>().levelData.Script;
        sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
        fadeManager = FindObjectOfType<FadeManager>();
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
            if (title.isTitle == false) // 수정필요
            {
                child.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                num++;
                if (num >= transform.childCount - 1)
                    num = 0;
                child = transform.GetChild(num).gameObject;

                childScript.text = child.GetComponent<LevelParent>().levelData.Script;
                sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (title.isTitle == false)
            {
                child.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                num--;
                if (num < 0)
                    num = transform.childCount - 2;
                child = transform.GetChild(num).gameObject;

                childScript.text = child.GetComponent<LevelParent>().levelData.Script;
                sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
            }
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
        SceneManager.LoadScene("stage");
        string a = child.GetComponent<LevelParent>().levelData.LevelName;
        switch(a)
        {
            case "1":
                //좌표넣기
                Debug.Log(a);
                break;
            case "2":
                break;
            case "3":
                break;
            case "4":
                break;
            case "5":
                break;
            case "6":
                break;
            case "7":
                break;
            case "8":
                break;
            case "9":
                break;
            case "10":
                break;

        }
        fadeManager.FadeIn();
        yield return new WaitForSeconds(0.5f);

    }
}
