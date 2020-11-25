using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3 : MonoBehaviour
{
    public GameObject tile;
    public float startPos_x;
    public float endPos_x;
    public float time = 0f;
    ButtonCount buttonCount;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            count += 1;
            if (count % 2 == 1)
            {
                iTween.MoveTo(tile, iTween.Hash("islocal", true, "x", endPos_x, "time", time,
                "easetype", iTween.EaseType.linear, "loopType", iTween.LoopType.none));
            }

            else
            { 
                iTween.MoveTo(tile, iTween.Hash("islocal", true, "x", startPos_x, "time", 1.5f,
                "easetype", iTween.EaseType.linear, "loopType", iTween.LoopType.none));
            }
        }
    }
}
