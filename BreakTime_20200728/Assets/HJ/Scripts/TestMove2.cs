using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove2 : MonoBehaviour
{
    public float maxSpeed;
    public float stopSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 키보드에서 손을 땠을 때 미끄러지면서 멈춤
        if (Input.GetButtonUp("Move2"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * stopSpeed, rb.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        // 움직임
        float h = Input.GetAxisRaw("Move2");
        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // 최대 속도 조절
        if (rb.velocity.x > maxSpeed)
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        else if (rb.velocity.x < maxSpeed * (-1))
            rb.velocity = new Vector2(maxSpeed * (-1), rb.velocity.y);
    }
}
