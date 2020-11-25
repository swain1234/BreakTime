using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button1 : MonoBehaviour
{
    public GameObject tile;
    public float startPos_y;
    public float endPos_y;
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
                iTween.MoveTo(tile, iTween.Hash("islocal", true, "y", endPos_y, "time", time,
                    "easetype", iTween.EaseType.linear, "loopType", iTween.LoopType.none));
            }

            else
            {
                iTween.MoveTo(tile, iTween.Hash("islocal", true, "y", startPos_y, "time", 1.5f,
                    "easetype", iTween.EaseType.linear, "loopType", iTween.LoopType.none));
            }
        }

    }
}
