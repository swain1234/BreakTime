using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Coffee.UIExtensions;

public class Option : MonoBehaviour
{
    static public Option instance;

    public Button retry;
    public Button select;
    public Button next;
    public Button xImage;

    public LevelData level1;
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
    private UIDissolve dissolve;

    public LevelData currentLevel;
    public LevelData nextLevel;

    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] Image panel;
    [SerializeField] Image candyImage;
    public bool isActive = false;
    bool isTransfer = false;
    public bool isCandy = false;

    private GameManager gameManager;
    private DialogueManager dialogueManager;
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
        xImage.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        retry.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        select.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        next.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        material = GetComponentInChildren<Image>().material;
        levelArray = new List<LevelData>();
        gameManager = FindObjectOfType<GameManager>();
        dissolve = stageText.GetComponent<UIDissolve>();
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
        dissolve.effectFactor = 1f;
        panel.gameObject.SetActive(false);
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
                    AudioManager.Instance.Pause();
                    AudioManager.Instance.Play("click");
                    Candy();
                    fade = 0f;
                    panel.gameObject.SetActive(true);
                    retry.gameObject.SetActive(true);
                    select.gameObject.SetActive(true);
                    next.gameObject.SetActive(true);
                    xImage.gameObject.SetActive(true);
                    isDissolving = true;
                }
                if (isDissolving)
                {
                    fade += Time.deltaTime;
                    dissolve.effectFactor -= Time.deltaTime;
                    if (fade > 1f)
                    {
                        fade = 1f;
                        dissolve.effectFactor = 0f;
                        isDissolving = false;
                        isActive = true;
                        StopScript.Instance.ScriptOFF();
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
                    AudioManager.Instance.UnPause();
                }
                if (isDissolving)
                {
                    fade -= Time.deltaTime;
                    dissolve.effectFactor += Time.deltaTime;
                    if (fade < 0f)
                    {
                        fade = 0f;
                        dissolve.effectFactor = 1f;
                        panel.gameObject.SetActive(false);
                        retry.gameObject.SetActive(false);
                        select.gameObject.SetActive(false);
                        next.gameObject.SetActive(false);
                        xImage.gameObject.SetActive(false);
                        isDissolving = false;
                        isActive = false;
                        dialogueManager = FindObjectOfType<DialogueManager>();
                        if (!dialogueManager.isDialogue)
                            StopScript.Instance.ScriptON();
                    }
                    material.SetFloat("_Fade", fade);
                }
            }
        }
    }
    public void Close()
    {
        if (isDissolving == false)
        {
            Time.timeScale = 1f;
            fade = 1f;
            isDissolving = true;
            AudioManager.Instance.UnPause();
            if (isDissolving)
            {
                fade -= Time.deltaTime;
                dissolve.effectFactor += Time.deltaTime;
                if (fade < 0f)
                {
                    fade = 0f;
                    dissolve.effectFactor = 1f;
                    panel.gameObject.SetActive(false);
                    retry.gameObject.SetActive(false);
                    select.gameObject.SetActive(false);
                    next.gameObject.SetActive(false);
                    xImage.gameObject.SetActive(false);
                    isDissolving = false;
                    isActive = false;
                    dialogueManager = FindObjectOfType<DialogueManager>();
                    if (!dialogueManager.isDialogue)
                        StopScript.Instance.ScriptON();
                }
                material.SetFloat("_Fade", fade);
            }
        }
    }

    public void StageScript()
    {
        if (currentLevel != null)
        {
            stageText.text = currentLevel.LevelName;
            for (int i = 0; i < levelArray.Count; i++)
            {
                if (currentLevel.LevelName == levelArray[i].LevelName)
                {
                    if (i != 9)
                    {
                        nextLevel = levelArray[i + 1];
                        break;
                    }
                    else
                        nextLevel = null;
                }
            }
        }
    }
    public void LevelChange()
    {
        if (nextLevel != null)
            currentLevel = nextLevel;
    }

    public void Retry()
    {
        if (!isDissolving)
        {
            Time.timeScale = 1f;
            if (isTransfer == false)
            {
                AudioManager.Instance.UnPause();
                FadeMusic();
            }
            isTransfer = true;
            isCandy = false;
            isActive = false;
            material.SetFloat("_Fade", 0f);
            dissolve.effectFactor = 1f;
            book = FindObjectOfType<AutoFlip>();
            LevelLoader.Instance.LoadLevel("Stage");
            book.transform.GetChild(0).gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
            isTransfer = false;
            gameManager = FindObjectOfType<GameManager>();
            gameManager.StartPosition();
        }
    }

    public void LevelSelect()
    {
        if (!isDissolving)
        {
            Time.timeScale = 1f;
            if (isTransfer == false)
            {
                AudioManager.Instance.UnPause();
                FadeMusic();
            }
            isTransfer = true;
            isCandy = false;
            isActive = false;
            material.SetFloat("_Fade", 0f);
            dissolve.effectFactor = 1f;
            LevelLoader.Instance.LoadLevel("Level");
            panel.gameObject.SetActive(false);
            isTransfer = false;
            Letterbox.Instance.initSetting();
        }
    }

    public void NextLevel()
    {
        if (!isDissolving)
        {
            Time.timeScale = 1f;
            if (currentLevel != level10)
            {
                StartCoroutine(LevelNext());
            }
        }
    }

    IEnumerator LevelNext()
    {
        if (isTransfer == false)
        {
            isTransfer = true;
            isCandy = false;
            LevelChange();
            StageScript();
            AudioManager.Instance.UnPause();
            FadeMusic();
            isActive = false;
            material.SetFloat("_Fade", 0f);
            dissolve.effectFactor = 1f;
            LevelLoader.Instance.LoadLevel("Level");
            book = FindObjectOfType<AutoFlip>();
            book.transform.GetChild(0).gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
            isTransfer = false;
            gameManager = FindObjectOfType<GameManager>();
            gameManager.NextStage();
            gameManager.StartPosition();
            Letterbox.Instance.initSetting();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Candy()
    {
        if (isCandy == true)
        {
            candyImage.gameObject.SetActive(true);
        }
    }

    void FadeMusic()
    {
        if (currentLevel == level1 || currentLevel == level2 == currentLevel == level3 == currentLevel == level4)
            AudioManager.Instance.FadeOut("Level1-4");
        else if (currentLevel == level5 || currentLevel == level6)
            AudioManager.Instance.FadeOut("Level5-6");
        else if (currentLevel == level7 || currentLevel == level7)
            AudioManager.Instance.FadeOut("Level7-8");
        else if (currentLevel == level9)
            AudioManager.Instance.FadeOut("Level9");
        else if (currentLevel == level10)
            AudioManager.Instance.BossStop();
    }
}
