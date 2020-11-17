using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyGoomba : MonoBehaviour
{
    // 현재 위치
    float currentPosition;

    // 위쪽 최대 이동값
    float upMax;
    // 아래쪽 최대 이동값
    float downMax;

    // 이동속도
    public float speed = -2.0f;
    // 이동 가능 범위
    public float maxRange;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position.y;

        upMax = transform.position.y + maxRange;
        downMax = transform.position.y - maxRange;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentPosition += Time.deltaTime * speed;

        if(currentPosition <= downMax)
        {
            speed *= -1;
            currentPosition = downMax;
        }
        else if (currentPosition >= upMax)
        {
            speed *= -1;
            currentPosition = upMax;
        }

        transform.position = new Vector3(transform.position.x, currentPosition, transform.position.z);
    }
}
