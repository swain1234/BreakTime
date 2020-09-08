using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pjump : MonoBehaviour
{
    public float PlayerJumpPower = 5f;
    public bool isJump = false;
    public int jumpCount = 0;
    private int count = 1;
    public float maxDistance = 0.80f;
    Animator animator;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        if (gameObject.tag == "Player1")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !animator.GetBool("isJump"))
            {
                if (jumpCount < count)
                {
                    isJump = true;
                    jumpCount++;
                    animator.SetBool("isJump", true);
                }
            }
        }
        else if (gameObject.tag == "Player2")
        {
            if (Input.GetKeyDown(KeyCode.W) && !animator.GetBool("isJump"))
            {
                if (jumpCount < count)
                {
                    isJump = true;
                    jumpCount++;
                    animator.SetBool("isJump", true);
                }
            }
        }
        Jump();
    }

    void Jump()
    {
        if (!isJump)
            return;
        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0f, PlayerJumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
        Debug.Log("jump");
        isJump = false;
    }

    // 레이케스트 사용
    // 바닥에 플레이어가 닿았는지 확인
    void CheckGround()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Doodle"));
        //int layerMask2 = (1 << LayerMask.NameToLayer("Player"));
        //RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector3.down, maxDistance, layerMask2);
        // bool isHit = Physics2D.Raycast(transform.position, Vector3.down, maxDistance);
        // (시작위치, 방향, 길이, 레이어마스크)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, maxDistance, layerMask);
        Debug.DrawRay(transform.position, Vector3.down * maxDistance, Color.red);

        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Ground") || hit.transform.CompareTag("Doodle"))
            {
                Debug.Log("ground");
                animator.SetBool("isJump", false);
                jumpCount = 0;
                return;
            }
        }
    }
    
}
