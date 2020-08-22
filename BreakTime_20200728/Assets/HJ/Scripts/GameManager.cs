using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stageIndex;
    public int Health;
    public PlayerMove player;
    public GameObject[] Stages;

    public void NextStage()
    {
        Stages[stageIndex].SetActive(false);
        stageIndex++;
        Stages[stageIndex].SetActive(true);
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
            collision.transform.position = new Vector3(0, 0, 0);
        }

    }

    public static void RestartStage()
    {
        Time.timeScale = 0f;

        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
