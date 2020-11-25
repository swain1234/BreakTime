using System.Collections;
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

    // 점프대에서 높이
    public float launcher;

    Animator animator;
    Rigidbody2D rigid;

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

        if(Input.GetKeyDown(KeyCode.P))
        {
            transform.position = new Vector3(58, 155, 0);
        }
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
            //else if (hit.transform.CompareTag("Gray"))
            //{
            //    hit.transform.gameObject.tag = "Obstacle";
            //}
        }
        else
            isGround = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump2") && isGround)
        {
            if (currentCount < jumpCount)
            {
                isJump = true;
                jumpCount++;
                animator.SetBool("isJump", true);
                animator.SetBool("isJumpUp", true);
                AudioManager.Instance.Play("jump2");
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }

        if (collision.gameObject.tag == "Launcher")
        {
            rigid.velocity = new Vector2(0, 0);
            Vector2 launch = new Vector2(0, launcher);
            rigid.AddForce(launch, ForceMode2D.Impulse);

            animator.SetBool("isJumpUp", true);
            animator.SetBool("isJump", true);
            animator.SetBool("isJumpDown", false);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Launcher")
    //    {
    //        rigid.velocity = new Vector2(0, 0);
    //        Vector2 launch = new Vector2(0, launcher);
    //        rigid.AddForce(launch, ForceMode2D.Impulse);

    //        animator.SetBool("isJumpUp", true);
    //        animator.SetBool("isJump", true);
    //        animator.SetBool("isJumpDown", false);
    //    }
    //}
}
