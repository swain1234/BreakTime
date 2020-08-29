using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeTarget : MonoBehaviour
{
    CinemachineVirtualCamera cine;
    private int count = 0;
    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Awake()
    {
        cine = GetComponent<CinemachineVirtualCamera>();
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        Change();
    }

    void Change()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;
        }

        if (count % 2 == 0)
        {
            cine.Follow = player1.transform;
        }
        else
        {
            cine.Follow = player2.transform;
        }
    }
}
