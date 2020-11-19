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
    public bool isGround = false;

    // 점프대에서 높이
    public float launcher;

    Animator animator;
    Rigidbody2D rigid;

    private HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        healthManager = FindObjectOfType<HealthManager>();
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
            if (hit.transform.CompareTag("Ground") || hit.transform.CompareTag("Black"))
            {
                //Debug.Log("Ground");
                animator.SetBool("isJump", false);
                animator.SetBool("isJumpDown", false);
                animator.SetBool("isJumpUp", false);
                isGround = true;
                currentCount = 0;
                return;
            }

            else if (hit.transform.CompareTag("Gray"))
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isJumpDown", false);
                animator.SetBool("isJumpUp", false);
                isGround = true;
                currentCount = 0;
                hit.transform.gameObject.tag = "Ground";

                return;
            }

            else if (hit.transform.CompareTag("Obstacle"))
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isJumpDown", false);
                animator.SetBool("isJumpUp", false);
                isGround = true;
                currentCount = 0;
                hit.transform.gameObject.tag = "Ground";
                return;
            }
        }
        else
            isGround = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !animator.GetBool("isJump") && isGround)
        {
            if (currentCount < jumpCount)
            {
                isJump = true;
                jumpCount++;
                animator.SetBool("isJump", true);
                animator.SetBool("isJumpUp", true);
                AudioManager.Instance.Play("jump1");
            }
        }
        Jump();


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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
}
