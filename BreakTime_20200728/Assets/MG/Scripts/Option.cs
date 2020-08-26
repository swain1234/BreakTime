using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Option : MonoBehaviour
{
    static public Option instance;

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

    public LevelData currentLevel;

    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] Image panel;
    bool isActive = false;

    private FadeManager fadeManager;

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
        fadeManager = FindObjectOfType<FadeManager>();
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
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    panel.gameObject.SetActive(true);
                    Time.timeScale = 0f;
                    isActive = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    panel.gameObject.SetActive(false);
                    Time.timeScale = 1f;
                    isActive = false;
                }
            }
        }
    }

    public void StageScript()
    {
        if (currentLevel != null)
        {
            stageText.text = currentLevel.LevelName;
        }
    }

    public void Retry()
    {
        StartCoroutine(RetryLevel());
    }

    IEnumerator RetryLevel()
    {
        panel.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isActive = false;
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        //초기셋팅상태로(위치)
        fadeManager.FadeIn();
        yield return new WaitForSeconds(0.5f);
    }

    public void LevelSelect()
    {
        StartCoroutine(SelectLevel());
    }

    IEnumerator SelectLevel()
    {
        panel.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isActive = false;
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level");
        fadeManager.FadeIn();
        yield return new WaitForSeconds(0.5f);
    }

    public void NextLevel()
    {
        StartCoroutine(LevelNext());
    }

    IEnumerator LevelNext()
    {
        panel.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isActive = false;
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        // 다음레벨 위치로
        fadeManager.FadeIn();
        yield return new WaitForSeconds(0.5f);
    }
}
