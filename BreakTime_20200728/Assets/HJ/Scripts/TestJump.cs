using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJump : MonoBehaviour
{
    public float jumpPower;
    public float rayDistance = 0.6f;
    public bool isJump;
    Rigidbody2D rigid;
    public int jumpCount;
    Animator animator;


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

    private void FixedUpdate()
    {
        if (gameObject.tag == "Player1")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !animator.GetBool("isJump"))
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                animator.SetBool("isJump", true);

            }
        }
        else if (gameObject.tag == "Player2")
        {
            if (Input.GetKeyDown(KeyCode.W) && !animator.GetBool("isJump"))
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                animator.SetBool("isJump", true);

            }
        }
        //Jump();

    }

    void Jump()
    {
        if (!isJump)
            return;
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        Debug.Log("jump");
        isJump = false;
    }

    void CheckGround()
    {
        Debug.DrawRay(rigid.position, Vector3.down, Color.red);

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, rayDistance, LayerMask.GetMask("Ground"));

        if (rayHit.collider != null)
        {
            if (rayHit.transform.CompareTag("Ground"))
            {
                Debug.Log("ground");
                animator.SetBool("isJump", false);
                return;
            }
        }
    }
}
