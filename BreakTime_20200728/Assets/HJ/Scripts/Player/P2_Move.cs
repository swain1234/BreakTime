using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class P2_Move : MonoBehaviour
{
    public float maxSpeed;
    public float stopSpeed;
    Rigidbody2D rigid;
    Animator animator;
    SkeletonAnimation skeleton;
    bool faceLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        skeleton = GetComponentInChildren<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        // 키보드에서 손을 땠을 때 미끄러지면서 멈춤
        if (Input.GetButtonUp("Move2"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * stopSpeed, rigid.velocity.y);
        }

        // 방향 전환 Default = left
        if (Input.GetAxisRaw("Move2") < 0 && !faceLeft)
        {
            // left
            skeleton.initialFlipX = false;
            Flip();
        }
        else if (Input.GetAxisRaw("Move2") > 0 && faceLeft)
        {
            // right
            skeleton.initialFlipX = true;
            Flip();
        }

        // 애니메이션
        // 점프 중에는 isMove - false
        if (animator.GetBool("isJump"))
        {
            animator.SetBool("isMove", false);

        }
        // 이동 중
        else
        {
            // 이동속도가 0.1 이하일 경우 isMove - false
            if (Mathf.Abs(rigid.velocity.x) < 0.3)
            {
                animator.SetBool("isMove", false);

            }
            // 그 외에는 isMove - true
            else
            {
                animator.SetBool("isMove", true);

            }

        }

    }

    private void FixedUpdate()
    {
        // 이동
        float move = Input.GetAxisRaw("Move2");
        rigid.AddForce(Vector2.right * move, ForceMode2D.Impulse);

        // 최대 속도 조절
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }

    void Flip()
    {
        faceLeft = !faceLeft;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

