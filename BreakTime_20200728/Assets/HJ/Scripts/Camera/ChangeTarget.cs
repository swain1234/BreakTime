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

    public SpriteRenderer renderer1;
    public SpriteRenderer renderer2;

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

        renderer1.color = zero;
        renderer2.color = zero;
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
        }
        else
        {
            cine.Follow = player2.transform;
            renderer1.color = zero;
            renderer2.color = one;
        }
    }

}
