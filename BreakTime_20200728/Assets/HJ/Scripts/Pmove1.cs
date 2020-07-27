using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pmove1 : MonoBehaviour
{
    public float PlayerMovePower;

    Rigidbody2D rigid;
    Vector3 movement;
    SpriteRenderer spriteRenderer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    rigid.velocity = new Vector2(rigid.velocity.normalized.x, rigid.velocity.y);
        //}

        // 애니메이션
        if (animator.GetBool("isJump") == true)
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            if (Mathf.Abs(rigid.velocity.x) < 0.3)
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
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if(Input.GetKey(KeyCode.RightArrow)==true)
        {
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = false;

        }
        else if (Input.GetKey(KeyCode.LeftArrow)==true)
        {
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = true;

        }

        transform.position += moveVelocity * PlayerMovePower * Time.deltaTime;

    }
}
