using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Vector3 startPos1;
    public Vector3 startPos2;

    public Quaternion startRotate1;
    public Quaternion startRotate2;

    bool isStart = false;

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
}
