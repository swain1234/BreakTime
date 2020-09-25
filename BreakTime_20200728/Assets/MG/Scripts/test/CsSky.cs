using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSky : MonoBehaviour
{

    [SerializeField] Transform[] tfBackgrounds = null;
    [SerializeField] float speed = 0f;

    float leftPosX = 0f;
    float rightPosX = 0f;

    private void Start()
    {
        float length = tfBackgrounds[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        leftPosX = -length;
        rightPosX = length * tfBackgrounds.Length;
        Debug.Log(tfBackgrounds.Length);
    }

    private void Update()
    {
        for(int i = 0; i< tfBackgrounds.Length; i++)
        {
            tfBackgrounds[i].position += new Vector3(speed, 0, 0) * Time.deltaTime;

            if(tfBackgrounds[i].position.x < leftPosX)
            {
                Vector3 selfPos = tfBackgrounds[i].position;
                selfPos.Set(selfPos.x + rightPosX, selfPos.y, selfPos.z);
                tfBackgrounds[i].position = selfPos;
            }
        }
    }

}
