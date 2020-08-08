using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TestMove : MonoBehaviour
{
    public float maxSpeed;
    public float stopSpeed;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 키보드에서 손을 땠을 때 미끄러지면서 멈춤
        if (Input.GetButtonUp("Move1"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * stopSpeed, rb.velocity.y);
        }

      

        if (Input.GetAxisRaw("Move1") < 0)
        {
            spriteRenderer.flipX = true;
            Debug.Log("LEFT");
        }
        else if (Input.GetAxisRaw("Move1") > 0)
        {
            spriteRenderer.flipX = false;
            Debug.Log("RIGHT");
        }

        // 애니메이션
        if (animator.GetBool("isJump") == true)
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            if (Mathf.Abs(rb.velocity.x) < 0.3)
            {
                animator.SetBool("isMove", false);
            }
            else
            {
                animator.SetBool("isMove", true);
            }
        }
        

    }

    private void FixedUpdate()
    {
        // 움직임
        float h = Input.GetAxisRaw("Move1");
        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // 최대 속도 조절
        if (rb.velocity.x > maxSpeed)
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        else if (rb.velocity.x < maxSpeed * (-1))
            rb.velocity = new Vector2(maxSpeed * (-1), rb.velocity.y);
    }
}
