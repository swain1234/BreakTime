using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour
{
    public GameObject tile;
    public bool moveRight = true;
    public float endPos = 0f;
    public float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        tile = GameObject.FindGameObjectWithTag("MoveTile");
    }

    // Update is called once per frame
    void Update()
    {
        iTween.MoveTo(tile, iTween.Hash("islocal", true, "x", endPos, "time", time,
            "easetype", iTween.EaseType.linear, "loopType", iTween.LoopType.pingPong));
        
    }
}
