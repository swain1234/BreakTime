using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump_1 : MonoBehaviour
{
    public float jumpPower = 14f;
    public float rayDistance = 0.4f;
    public float rayPosition = -0.15f;
    public int candyValue = 100;
    public bool isGround = false;
    public int jumpCount = 0;
    // 점프대에서 높이
    public float launcher = 22f;

    public bool isJump = false;

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
                isJump = false;
                jumpCount = 0;
                return;
            }

            else if (hit.transform.CompareTag("Gray"))
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isJumpDown", false);
                animator.SetBool("isJumpUp", false);
                isGround = true;
                hit.transform.gameObject.tag = "Ground";
                isJump = false;
                jumpCount = 0;
                return;
            }
        }
        else
        {
            isGround = false;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Jump1") == 1 && jumpCount == 0)
        {
            isJump = true;
            //isGround = false;
            jumpCount += 1;
        }
        else if (Input.GetAxisRaw("Jump1") < 0.99)
        {
            isJump = false;
            //isGround = true;
            jumpCount = 0;
        }
        
        if (isGround && isJump)
        {
            animator.SetBool("isJump", true);
            animator.SetBool("isJumpUp", true);
            AudioManager.Instance.Play("jump1");

            Vector2 jumpVelocity = new Vector2(0f, jumpPower);
            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isGround = false;
        }

        // JumpDown
        if (rigid.velocity.y < -0.05f)
        {
            animator.SetBool("isJumpDown", true);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
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
