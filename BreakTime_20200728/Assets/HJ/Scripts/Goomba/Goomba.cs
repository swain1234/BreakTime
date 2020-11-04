using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    // 현재 위치
    float currentPosition;
    // 왼쪽으로 이동 가능한 최대값
    public float leftMax = -3.0f;
    // 오른쪽으로 이동 가능한 최대값
    public float rightMax = 3.0f;
    // 이동속도
    public float direction = 2.0f;

    public float maxRange = 3.0f;

    float maxR;
    float maxL;

    Rigidbody2D rigid;
    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        currentPosition = transform.position.x;
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        maxR = transform.position.x + maxRange;
        maxL = transform.position.x - maxRange;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentPosition += Time.deltaTime * direction;

        if(currentPosition >= maxR)
        {
            direction *= -1;
            currentPosition = maxR;
            renderer.flipX = false;
        }

        else if(currentPosition <= maxL)
        {
            direction *= -1;
            currentPosition = maxL;
            renderer.flipX = true;
        }

        transform.position = new Vector3(currentPosition, transform.position.y, transform.position.z);
    }


}
