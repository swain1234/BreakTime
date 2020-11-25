using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWall : MonoBehaviour
{
    public GameObject tile;
    public float endPos_y;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player2") || collision.transform.CompareTag("Player1"))
        {
            iTween.MoveTo(tile, iTween.Hash("islocal", true, "y", endPos_y, "time", time,
                "easetype", iTween.EaseType.linear, "loopType", iTween.LoopType.none));
        }
    }
}
