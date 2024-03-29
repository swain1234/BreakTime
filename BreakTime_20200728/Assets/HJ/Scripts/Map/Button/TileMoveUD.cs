﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoveUD : MonoBehaviour
{
    public GameObject tile;
    public bool moveRight = true;
    public float endPos = 0f;
    public float time = 0f;
    public float endPos1 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        tile = GameObject.FindGameObjectWithTag("MoveTile");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        iTween.MoveTo(tile, iTween.Hash("islocal", true, "y", endPos, "time", time,
    "easetype", iTween.EaseType.linear, "loopType", iTween.LoopType.pingPong));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player2"))
        {
            collision.transform.SetParent(transform);
        }
        else if (collision.transform.CompareTag("Player1"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player2"))
        {
            collision.transform.SetParent(null);
        }

        else if (collision.transform.CompareTag("Player1"))
        {
            collision.transform.SetParent(null);
        }
    }
}
