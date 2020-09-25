using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Option : MonoBehaviour
{
    static public Option instance;

    public Button retry;
    public Button select;
    public Button next;

    [SerializeField] LevelData level1;
    [SerializeField] LevelData level2;
    [SerializeField] LevelData level3;
    [SerializeField] LevelData level4;
    [SerializeField] LevelData level5;
    [SerializeField] LevelData level6;
    [SerializeField] LevelData level7;
    [SerializeField] LevelData level8;
    [SerializeField] LevelData level9;
    [SerializeField] LevelData level10;

    List<LevelData> levelArray; // 레벨데이터를 담는 리스트

    Material material;

    bool isDissolving = false;
    float fade;

    public LevelData currentLevel;
    public LevelData nextLevel;

    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] Image panel;
    [SerializeField] Image candyImage;
    bool isActive = false;
    public bool isCandy = false;

    private FadeManager fadeManager;
    private GameManager gameManager;
    private AutoFlip book;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        retry.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        select.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        next.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        material = GetComponentInChildren<Image>().material;
        levelArray = new List<LevelData>();
        fadeManager = FindObjectOfType<FadeManager>();
        gameManager = FindObjectOfType<GameManager>();
        for (var i = 0; i < 10; i++)
        {
            levelArray.Add((level1));
            levelArray.Add((level2));
            levelArray.Add((level3));
            levelArray.Add((level4));
            levelArray.Add((level5));
            levelArray.Add((level6));
            levelArray.Add((level7));
            levelArray.Add((level8));
            levelArray.Add((level9));
            levelArray.Add((level10));
        }
        material.SetFloat("_Fade", 0f);
    }

    void Update()
    {
        OptionActivate();
    }

    private void OptionActivate()
    {
        if (SceneManager.GetActiveScene().name == "Stage")
        {
            if (isActive == false)
            {
                if (Input.GetKeyDown(KeyCode.Escape) && isDissolving == false)
                {
                    Candy();
                    fade = 0f;
                    retry.gameObject.SetActive(true);
                    select.gameObject.SetActive(true);
                    next.gameObject.SetActive(true);
                    isDissolving = true;
                }
                if (isDissolving)
                {
                    fade += Time.deltaTime;
                    if (fade >= 1f)
                    {
                        fade = 1f;
                        isDissolving = false;
                        isActive = true;
                        Time.timeScale = 0f;
                    }
                    material.SetFloat("_Fade", fade);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape) && isDissolving == false)
                {
                    Time.timeScale = 1f;
                    fade = 1f;
                    isDissolving = true;
                }
                if (isDissolving)
                {
                    fade -= Time.deltaTime;

                    if (fade <= 0f)
                    {
                        fade = 0f;
                        retry.gameObject.SetActive(false);
                        select.gameObject.SetActive(false);
                        next.gameObject.SetActive(false);
                        isDissolving = false;
                        isActive = false;
                    }
                    material.SetFloat("_Fade", fade);
                }
            }

        }
    }

    public void StageScript()
    {
        if (currentLevel != null)
        {
            stageText.text = currentLevel.LevelName;
            for(int i = 0; i < levelArray.Count; i++)
            {
                if (currentLevel.LevelName == levelArray[i].LevelName && i < 9)
                    nextLevel = levelArray[i + 1];
            }
        }
    }
    public void LevelChange()
    {
        if (nextLevel != null)
        {
            currentLevel = nextLevel;
            for (int i = 0; i < levelArray.Count; i++)
            {
                if (currentLevel.LevelName == levelArray[i].LevelName && i < 9)
                    nextLevel = levelArray[i + 1];
            }
        }
    }

    public void Retry()
    {
        StartCoroutine(RetryLevel());
    }

    IEnumerator RetryLevel()
    {
        isCandy = false;
        Time.timeScale = 1f;
        isActive = false;
        material.SetFloat("_Fade", 0f);
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        book = FindObjectOfType<AutoFlip>();
        book.transform.GetChild(0).gameObject.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
        gameManager.StartPosition();
        fadeManager.FadeIn();
    }

    public void LevelSelect()
    {
        StartCoroutine(SelectLevel());
    }

    IEnumerator SelectLevel()
    {
        isCandy = false;
        Time.timeScale = 1f;
        isActive = false;
        material.SetFloat("_Fade", 0f);
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        fadeManager.FadeIn();
        SceneManager.LoadScene("Level");
    }

    public void NextLevel()
    {
        StartCoroutine(LevelNext());
    }

    IEnumerator LevelNext()
    {
        isCandy = false;
        LevelChange();
        Time.timeScale = 1f;
        isActive = false;
        material.SetFloat("_Fade", 0f);
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        book = FindObjectOfType<AutoFlip>();
        book.transform.GetChild(0).gameObject.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
        gameManager.NextStage();
        gameManager.StartPosition();
        fadeManager.FadeIn();
        yield return new WaitForSeconds(0.5f);
    }

    public void Candy()
    {
        if(isCandy == true)
        {
            candyImage.gameObject.SetActive(true);
        }
    }
}
