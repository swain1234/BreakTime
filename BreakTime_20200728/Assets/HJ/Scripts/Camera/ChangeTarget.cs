using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeTarget : MonoBehaviour
{
    CinemachineVirtualCamera cine;
    CinemachineConfiner confiner;

    private int count = 0;
    public GameObject player1;
    public GameObject player2;
    public GameObject BossMode;

    public GameObject current1;
    public GameObject current2;

    public SpriteRenderer renderer1;
    public SpriteRenderer renderer2;

    Color one = new Vector4(1, 1, 1, 1);
    Color zero = new Vector4(1, 1, 1, 0);

    public PolygonCollider2D[] Range;
    Option option;
    public int stageIndex;
    [SerializeField] int tempNum;

    // Start is called before the first frame update
    void Awake()
    {
        cine = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();
        
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        BossMode = GameObject.FindGameObjectWithTag("BossMode");

        renderer1 = current1.GetComponent<SpriteRenderer>();
        renderer2 = current2.GetComponent<SpriteRenderer>();

        renderer1.color = zero;
        renderer2.color = zero;

        option = FindObjectOfType<Option>();

        if (option != null)
        {
            string[] s = option.currentLevel.LevelName.Split('_');
            stageIndex = int.Parse(s[0]) - 1;
        }
        else
        {
            stageIndex = tempNum;
        }
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

        if(stageIndex == 8 || stageIndex == 9)
        {
            cine.Follow = BossMode.transform;
            renderer1.color = zero;
            renderer2.color = zero;
        }

        else
        {
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

    public void test(int index)
    {
        for(int i =0; i < Range.Length; i++)
        {
            if(i == stageIndex)
            {
                confiner.m_BoundingShape2D = Range[i];
            }
            
        }
    }
}
