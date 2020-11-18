using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    CinemachineConfiner confiner;

    public P_Move player1;
    public P_Move player2;

    [SerializeField] StopManager stopManager;
    [SerializeField] int tempNum = 5;

    public GameObject[] Stages;
    public GameObject[] Backgrounds;
    public Transform[] StartPositions;

    public int stageIndex;

    public Vector3 startPos;
    Quaternion startRotate;
    bool isStart = true;

    DialogueManager dialogueManager;
    Option option;
    ChangeTarget changeTarget;

    private void Start()
    {
        confiner = GetComponent<CinemachineConfiner>();

        // Scene간의 연결 오브젝트는 싱글톤으로 만들 것
        // stageIndex = Linker.Instance.pickStageIndex;

        startPos = GameObject.FindGameObjectWithTag("Start1").transform.position;
        startRotate = GameObject.FindGameObjectWithTag("Start1").transform.rotation;

        dialogueManager = FindObjectOfType<DialogueManager>();
        option = FindObjectOfType<Option>();
        changeTarget = FindObjectOfType<ChangeTarget>();

        if (option != null)
        {
            string[] s = option.currentLevel.LevelName.Split('_');
            stageIndex = int.Parse(s[0]) - 1;
        }
        else
        {
            stageIndex = tempNum;
        }
        StartPosition();

    }

    public void StartPosition()
    {
        stopManager.ScriptON();
        player1.transform.position = StartPositions[stageIndex * 2].position;
        player2.transform.position = StartPositions[stageIndex * 2 + 1].position;
        OnStage(stageIndex);
        changeTarget.test(stageIndex);

        if (isStart == true)
        {
            stopManager.ScriptOFF();
            StartCoroutine(Dialogue());
            isStart = false;
        }
    }

    IEnumerator Dialogue()
    {
        yield return new WaitForSeconds(0.1f);
        dialogueManager.ReadDialogue(0);
    }

    public void NextStage()
    {
        // 스테이지 이동
        if(stageIndex < Stages.Length-1)
        {
            Stages[stageIndex].SetActive(false);
            Backgrounds[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            Backgrounds[stageIndex].SetActive(true);

            isStart = true;
        }
    }

    public void Clear()
    {
        dialogueManager.ReadDialogue(1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            // 플레이어 원위치
            //collision.attachedRigidbody.velocity = Vector2.zero;
            collision.transform.position = StartPositions[stageIndex * 2].position;
        }

        else if (collision.gameObject.tag == "Player2")
        {
            //collision.attachedRigidbody.velocity = Vector2.zero;
            collision.transform.position = StartPositions[stageIndex * 2 + 1].position;

        }
    }

    public void OnStage(int index)
    {
        for(int i = 0; i < Stages.Length; i++)
        {
            if(i == stageIndex)
            {
                Stages[i].SetActive(true);
                Backgrounds[i].SetActive(true);

            }

            else
            {
                Stages[i].SetActive(false);
                Backgrounds[i].SetActive(false);

            }
        }
    }

}
