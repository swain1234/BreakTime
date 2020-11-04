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

    public GameObject current1;
    public GameObject current2;

    SpriteRenderer renderer1;
    SpriteRenderer renderer2;

    Color one = new Vector4(1, 1, 1, 1);
    Color zero = new Vector4(1, 1, 1, 0);

    // Start is called before the first frame update
    void Awake()
    {
        cine = GetComponent<CinemachineVirtualCamera>();
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        renderer1 = current1.GetComponent<SpriteRenderer>();
        renderer2 = current2.GetComponent<SpriteRenderer>();
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
            renderer1.color = one;
            renderer2.color = zero;

            Invoke("Fade1", 1f);
        }
        else
        {
            cine.Follow = player2.transform;
            renderer1.color = zero;
            renderer2.color = one;

            Invoke("Fade2", 1f);
        }
    }

    void Fade1()
    {

        renderer1.color = zero;
    }

    void Fade2()
    {

        renderer2.color = zero;
    }

}
