using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Health;
    public PlayerMove player;
    public GameObject[] Stages;
    public int stageIndex;
    public Vector3 startPos;
    Quaternion startRotate;
    bool isStart = false;

    private void Start()
    {
        startPos = GameObject.FindGameObjectWithTag("Start").transform.position;
        startRotate = GameObject.FindGameObjectWithTag("Start").transform.rotation;
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

            //
        }
    }

    public void HealthDown()
    {
        if (Health > 0)
            Health--;
        else
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            Health--;

            // 플레이어 원위치
            collision.attachedRigidbody.velocity = Vector2.zero;
            collision.transform.position = new Vector3(0, 0, -1);
        }

    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
         
    }

    void StartGame()
    {
        GameObject startCam = GameObject.FindGameObjectWithTag("MainCamera");
        startCam.SetActive(false);

        startPos = new Vector3(startPos.x, startPos.y + 1f, startPos.z);
        Instantiate(player, startPos, startRotate);
    }
}
