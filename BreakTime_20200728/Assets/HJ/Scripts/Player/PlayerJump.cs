using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpPower = 5f;
    public int currentCount = 0;
    public int jumpCount = 1;
    public float rayDistance;
    public bool isJump = false;
    // ray 시작위치 조절
    public float rayPosition = 0.3f;
    public int candyValue = 100;

    Animator animator;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        RaycastHit2D hit = Physics2D.Raycast(rayPos , Vector3.down, rayDistance, layerMask);
        Debug.DrawRay(rayPos, Vector3.down * rayDistance, Color.red);

        if(hit.collider != null)
        {
            if(hit.transform.CompareTag("Ground") || hit.transform.CompareTag("MoveTile"))
            {
                //Debug.Log("Ground");
                animator.SetBool("isJump", false);
                animator.SetBool("isJumpDown", false);
                animator.SetBool("isJumpUp", false);
                currentCount = 0;
                return;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if(gameObject.tag == "Player1")
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) && !animator.GetBool("isJump"))
            {
                if(currentCount < jumpCount)
                {
                    isJump = true;
                    jumpCount++;
                    animator.SetBool("isJump", true);
                    animator.SetBool("isJumpUp", true);
                }
            }
        }
        else if(gameObject.tag == "Player2")
        {
            if(Input.GetKeyDown(KeyCode.W) && !animator.GetBool("isJump"))
            {
                if(currentCount < jumpCount)
                {
                    isJump = true;
                    jumpCount++;
                    animator.SetBool("isJump", true);
                    animator.SetBool("isJumpUp", true);
                }
            }
        }

        Jump();
        
        // JumpDown
        if (rigid.velocity.y < -0.01)
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
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // 사탕 먹기
    //    if (collision.gameObject.tag == "Candy")
    //    {
    //        ScoreManager.setCandy(candyValue);

    //        // 닿으면 삭제
    //        Destroy(collision.gameObject, 0f);
    //    }
    //}
}
