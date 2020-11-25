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
    bool isFlip = false;

    Animator pieceAnimator;

    string currentState;
    const string LevelPiece = "LevelPiece";
    const string LevelPieceFlip = "LevelPieceFlip";

    [SerializeField] TextMeshProUGUI resourceText;
    [SerializeField] GameObject choice;
    [SerializeField] Image piece;

    void Start()
    {
        sceneChange = GetComponentInParent<SceneChanage>();
        child = transform.GetChild(num).gameObject;
        resourceText.text = child.GetComponent<LevelParent>().levelData.Script;
        sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
        option = FindObjectOfType<Option>();
        title = FindObjectOfType<Title>();
        pieceAnimator = piece.GetComponent<Animator>();
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
        num++;
        if (num >= transform.childCount)
            num = 0;
        MovePiece();
        child = transform.GetChild(num).gameObject;

        resourceText.text = child.GetComponent<LevelParent>().levelData.Script;
        sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
    }

    public void PreviousButton()
    {
        num--;
        if (num < 0)
            num = transform.childCount - 1;
        MovePiece();
        child = transform.GetChild(num).gameObject;

        resourceText.text = child.GetComponent<LevelParent>().levelData.Script;
        sampleImage.sprite = child.GetComponent<LevelParent>().levelData.Icon;
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        pieceAnimator.Play(newState);

        currentState = newState;
    }

    private void FlipPiece()
    {
        Vector3 scale = piece.rectTransform.localScale;
        if (piece.transform.localScale.x > 0)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);
        piece.rectTransform.localScale = scale;
    }

    public void MovePiece()
    {
        if (num == 0)
        {
            piece.rectTransform.anchoredPosition = new Vector2(981f, -275f);
            ChangeAnimationState(LevelPiece);
        }
        else if (num == 1)
        {
            piece.rectTransform.anchoredPosition = new Vector2(2441f, 305f);
            ChangeAnimationState(LevelPieceFlip);
            FlipPiece();
            isFlip = true;
            return;
        }
        else if(num == 2)
        {
            piece.rectTransform.anchoredPosition = new Vector2(1015f, 271f);
            ChangeAnimationState(LevelPiece);
        }
        else
        {
            piece.rectTransform.anchoredPosition = new Vector2(1750f, 895f);
            ChangeAnimationState(LevelPiece);
        }

        if (isFlip)
        {
            FlipPiece();
            isFlip = false;
        }
    }

    public void Close()
    {
        RectTransform rect = choice.GetComponent<RectTransform>();
        rect.offsetMin = new Vector2(5000, rect.offsetMin.y); //Left 5000
        rect.offsetMax = new Vector2(5000, rect.offsetMax.y); //Right -5000
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
