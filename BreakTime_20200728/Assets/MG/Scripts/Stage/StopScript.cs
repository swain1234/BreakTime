using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopScript : MonoBehaviour
{
    public static StopScript instance;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossBG;
    [SerializeField] GameObject bossCam;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public static StopScript Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void ScriptON()
    {
        player1.GetComponent<P_Move>().enabled = true;
        player2.GetComponent<P_Move>().enabled = true;
        player1.GetComponent<PlayerJump>().enabled = true;
        player2.GetComponent<Player2Jump>().enabled = true;
        mainCamera.GetComponent<ChangeTarget>().enabled = true;

    }

    public void ScriptOFF()
    {
        player1.GetComponent<P_Move>().enabled = false;
        player2.GetComponent<P_Move>().enabled = false;
        player1.GetComponent<PlayerJump>().enabled = false;
        player2.GetComponent<Player2Jump>().enabled = false;
        mainCamera.GetComponent<ChangeTarget>().enabled = false;

    }

    // 보스전에 오른쪽으로 계속 움직이는 스크립트
    // 대화 끝나고 3초(보스등장) 뒤에 움직임
    public void BossON()
    {
        bossBG.GetComponent<BossBG>().enabled = true;
        bossCam.GetComponent<BossCam>().enabled = true;
    }

    // 보스 몸통. 대화 끝나자마자 바로 3초간 애니메이션
    public void onlyBoss()
    {
        boss.gameObject.SetActive(true);
    }

    public void BossOFF()
    {
        boss.gameObject.SetActive(false);
        bossBG.GetComponent<BossBG>().enabled = false;
        bossCam.GetComponent<BossCam>().enabled = false;
    }
}
