using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RangeCtrol : MonoBehaviour
{
    CinemachineVirtualCamera cine;
    CinemachineConfiner confiner;

    [SerializeField] GameManager gameManager;
    public GameObject[] Ranges;
    public int index;

    // Start is called before the first frame update
    void Awake()
    {
        cine = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();

        index = gameManager.stageIndex;
    }

    public void RangeChange()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
