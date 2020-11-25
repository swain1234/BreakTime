using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopManager : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject mainCamera;


    public void ScriptON()
    {
        player1.GetComponent<P_Move>().enabled = true;
        player2.GetComponent<P_Move>().enabled = true;
        player1.GetComponent<PlayerJump_1>().enabled = true;
        player2.GetComponent<PlayerJump_2>().enabled = true;
        mainCamera.GetComponent<ChangeTarget>().enabled = true;
    }

    public void ScriptOFF()
    {
        player1.GetComponent<P_Move>().enabled = false;
        player2.GetComponent<P_Move>().enabled = false;
        player1.GetComponent<PlayerJump_1>().enabled = false;
        player2.GetComponent<PlayerJump_2>().enabled = false;
        mainCamera.GetComponent<ChangeTarget>().enabled = false;
    }
}
