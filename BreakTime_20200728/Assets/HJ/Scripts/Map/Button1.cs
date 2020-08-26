﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button1 : MonoBehaviour
{
    public GameObject tile;
    public bool moveUp = true;
    public float endPos = 0f;
    public float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player2") || collision.transform.CompareTag("Player1"))
        {
            iTween.MoveTo(tile, iTween.Hash("islocal", true, "y", endPos, "time", time,
                "easetype", iTween.EaseType.linear, "loopType", iTween.LoopType.none));
        }
    }
}
