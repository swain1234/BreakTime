﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Jump : MonoBehaviour
{
    public float jumpPower = 5f;
    public int currentCount = 0;
    public int jumpCount = 1;
    public float rayDistance;
    public bool isJump = false;
    // ray 시작위치 조절
    public float rayPosition = 0.3f;
    public int candyValue = 100;
    public bool isGround = false;

    Animator animator;
    Rigidbody2D rigid;
    public GameObject grayTile;

    private HealthManager2 healthManager;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        healthManager = FindObjectOfType<HealthManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
    }

    // 레이캐스트
    void CheckGround()
    {
        Vector2 rayPos = new Vector3(transform.position.x, transform.position.y - rayPosition);
        int layerMask = (1 << LayerMask.NameToLayer("Ground"));
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector3.down, rayDistance, layerMask);
        Debug.DrawRay(rayPos, Vector3.down * rayDistance, Color.red);

        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Ground"))
            {
                //Debug.Log("Ground");
                animator.SetBool("isJump", false);
                animator.SetBool("isJumpDown", false);
                animator.SetBool("isJumpUp", false);
                isGround = true;
                currentCount = 0;
                return;
            }
            else if (hit.transform.CompareTag("Gray") || hit.transform.CompareTag("Black"))
            {

                hit.transform.gameObject.tag = "Obstacle";
            }
        }
        else
            isGround = false;
    }

    private void FixedUpdate()
    {

        if (isGround)
        {
            //if (gameObject.tag == "Player1")
            //{
            //    if (Input.GetKeyDown(KeyCode.UpArrow) && !animator.GetBool("isJump"))
            //    {
            //        if (currentCount < jumpCount)
            //        {
            //            isJump = true;
            //            jumpCount++;
            //            animator.SetBool("isJump", true);
            //            animator.SetBool("isJumpUp", true);
            //        }
            //    }
            //}
            //else if (gameObject.tag == "Player2")
            //{
                if (Input.GetKeyDown(KeyCode.W) && !animator.GetBool("isJump"))
                {
                    if (currentCount < jumpCount)
                    {
                        isJump = true;
                        jumpCount++;
                        animator.SetBool("isJump", true);
                        animator.SetBool("isJumpUp", true);
                    }
                }
            //}
            Jump();
        }

        // JumpDown
        if (rigid.velocity.y < -0.05f)
        {
            animator.SetBool("isJumpDown", true);
            //animator.SetBool("isJump", true);
        }

    }

    void Jump()
    {
        if (!isJump)
            return;
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0f, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        isJump = false;
        isGround = false;
    }

}