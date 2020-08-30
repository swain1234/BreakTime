using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public P_Move player1;
    public P_Move player2;

    public GameObject[] Stages;
    public int stageIndex;

    public Transform[] StartPositions;

    public Vector3 startPos1;
    public Vector3 startPos2;

    public Vector3 startPos;
    Quaternion startRotate;
    bool isStart = false;

    private void Start()
    {

        // Scene간의 연결 오브젝트는 싱글톤으로 만들 것
        // stageIndex = Linker.Instance.pickStageIndex;
        //
        startPos = GameObject.FindGameObjectWithTag("Start1").transform.position;
        startRotate = GameObject.FindGameObjectWithTag("Start1").transform.rotation;

        player1.transform.position = StartPositions[stageIndex * 2].position;
        player2.transform.position = StartPositions[stageIndex * 2 + 1].position;
        OnStage(stageIndex);

    }


    public void NextStage()
    {
        // 스테이지 이동
        if(stageIndex < Stages.Length-1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
        }
        else
        {
            // 클리어

            Debug.Log("Clear!");
            // 플레이어 정지
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1")
        {
            // 플레이어 원위치
            collision.attachedRigidbody.velocity = Vector2.zero;
            collision.transform.position = StartPositions[stageIndex*2].position;
        }

        else if(collision.gameObject.tag == "Player2")
        {
            collision.attachedRigidbody.velocity = Vector2.zero;
            collision.transform.position = StartPositions[stageIndex*2+1].position;

        }

    }

    void OnStage(int index)
    {
        for(int i = 0; i < Stages.Length; i++)
        {
            if( i == stageIndex)
            {
                Stages[i].SetActive(true);
            }
            else
            {
                Stages[i].SetActive(false);
            }
        }
    }

//    void PlayerReposition()
//    {
//        player1.transform.position = new Vector3(0, 0, -1);
         
//    }

//    void StartGame()
//    {
//        GameObject startCam = GameObject.FindGameObjectWithTag("MainCamera");
//        startCam.SetActive(false);

//        startPos = new Vector3(startPos.x, startPos.y + 1f, startPos.z);
//        Instantiate(player1, startPos, startRotate);
//    }
}
