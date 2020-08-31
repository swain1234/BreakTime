using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public Vector3 startPos1;
    public Vector3 startPos2;

    public Quaternion startRotate1;
    public Quaternion startRotate2;

    bool isStart = false;
    static bool isEnd = false;

    [SerializeField] GameObject[] stages;
    static int currentStage = 0;

    [SerializeField] Transform originPos;
    [SerializeField] Rigidbody playerRigid;


    // Start is called before the first frame update
    void Start()
    {
        startPos1 = GameObject.FindGameObjectWithTag("Start1").transform.position;
        startRotate1 = GameObject.FindGameObjectWithTag("Start1").transform.rotation;

        startPos2 = GameObject.FindGameObjectWithTag("Start2").transform.position;
        startRotate2 = GameObject.FindGameObjectWithTag("Start2").transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        startPos1 = new Vector3(startPos1.x, startPos1.y + 1f, startPos1.z);
        startPos2 = new Vector3(startPos2.x, startPos2.y + 1f, startPos2.z);

        Instantiate(player1, startPos1, startRotate1);
        Instantiate(player2, startPos2, startRotate2);
    }
    public void NextStage()
    {
        playerRigid.gameObject.transform.position = originPos.position;
        stages[currentStage++].SetActive(false);
        stages[currentStage].SetActive(true);
    }

    public static void RestartStage()
    {
        
    }

    public static void EndGame()
    {
        Time.timeScale = 0f;

        isEnd = true;
        
    }
}
