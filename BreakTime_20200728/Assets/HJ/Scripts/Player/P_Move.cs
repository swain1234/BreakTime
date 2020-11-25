﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Cinemachine;

public class P_Move : MonoBehaviour
{
    public ScoreManager scoreManager;
    public GameManager gameManager;
    public float maxSpeed;
    public float stopSpeed;
    Rigidbody2D rigid;
    Animator animator;
    SkeletonAnimation skeleton;
    bool faceRight = true;
    Option option;
    [SerializeField] StopManager stopManager;

    public bool isAttack = false;
    public int skillCount1 = 0;
    public bool isHide = false;
    public int skillCount2 = 0;
    public GameObject range;

    public bool isTouch = false;
    public bool isPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        option = FindObjectOfType<Option>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        skeleton = GetComponentInChildren<SkeletonAnimation>();
        isTouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Player1")
        {
            // 키보드에서 손을 땠을 때 미끄러지면서 멈춤
            if (Input.GetButtonUp("Move1"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * stopSpeed, rigid.velocity.y);
                //AudioManager.instance.Stop("walk1");
            }

            // 방향 전환 Default = left
            if (Input.GetAxisRaw("Move1") > 0 && !faceRight)
            {
                // left
                skeleton.initialFlipX = true;
                Flip();
            }
            else if (Input.GetAxisRaw("Move1") < 0 && faceRight)
            {
                // right
                skeleton.initialFlipX = false;
                Flip();
            }
        }
        else if(gameObject.tag == "Player2")
        {
            // 키보드에서 손을 땠을 때 미끄러지면서 멈춤
            if (Input.GetButtonUp("Move2"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * stopSpeed, rigid.velocity.y);
                //AudioManager.instance.Stop("walk2");
            }

            // 방향 전환 Default = right
            if (Input.GetAxisRaw("Move2") > 0 && !faceRight)
            {
                // left
                skeleton.initialFlipX = true;
                Flip();
                
            }
            else if (Input.GetAxisRaw("Move2") < 0 && faceRight)
            {
                // right
                skeleton.initialFlipX = false;
                Flip();

            }
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
            if (Mathf.Abs(rigid.velocity.x) < 0.15)
            {
                animator.SetBool("isMove", false);
            }
            // 그 외에는 isMove - true
            else
            {
                animator.SetBool("isMove", true);
                animator.SetBool("isHide", false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameObject.tag == "Player1")
        {
            //if (Input.GetAxisRaw("Move1") == 1)
            //{
            //    AudioManager.instance.Play("walk1");
            //    isPlay = true;

            //}
            //else if (Input.GetAxisRaw("Move1") < 0.99 || !animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
            //{
            //    AudioManager.instance.Stop("walk1");
            //    isPlay = false;
            //}
            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                AudioManager.instance.Play("walk1");
                isPlay = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
            {
                AudioManager.instance.Stop("walk1");
                isPlay = false;
            }

            // 이동
            float move = Input.GetAxisRaw("Move1");
            rigid.AddForce(Vector2.right * move, ForceMode2D.Impulse);

            // 최대 속도 조절
            if (rigid.velocity.x > maxSpeed)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxSpeed * (-1))
            {
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
            }

            // 공격
            if (Input.GetAxisRaw("Skill1") == 1 && skillCount1 == 0)
            {
                isAttack = true;
                skillCount1 += 1;
            }
            else if (Input.GetAxisRaw("Skill1") < 0.99)
            {
                isAttack = false;
                skillCount1 = 0;
            }

            if (isAttack && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                animator.SetTrigger("Attack");
                //isAttack = true;
                range.SetActive(true);
                AudioManager.Instance.Play("brush");
                AudioManager.instance.Stop("walk1");

                rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                isAttack = false;
                range.SetActive(false);

                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

            }
        }

        else if (gameObject.tag == "Player2")
        {
            //if (Input.GetAxisRaw("Move2") == 1)
            //{
            //    AudioManager.instance.Play("walk2");
            //    isPlay = true;

            //}
            //else if(Input.GetAxisRaw("Move2") < 0.99 || !animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
            //{
            //    AudioManager.instance.Stop("walk2");
            //    isPlay = false;
            //}
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
            {
                AudioManager.instance.Play("walk2");
                isPlay = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
            {
                AudioManager.instance.Stop("walk2");
                isPlay = false;
            }

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

            // 무적스킬
            if (Input.GetAxisRaw("Skill2") == 1 && skillCount2 == 0)
            {
                isHide = true;
                skillCount2 += 1;
            }
            else if (Input.GetAxisRaw("Skill2") < 0.99)
            {
                isHide = false;
                skillCount2 = 0;
            }
            if (isHide &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName("action") &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName("action_loof"))
            {
                animator.SetTrigger("Skill");
                isHide = true;
                AudioManager.Instance.Play("bush");
                AudioManager.instance.Stop("walk2");

                CapsuleCollider2D capsule = gameObject.GetComponent<CapsuleCollider2D>();
                capsule.enabled = false;
                CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
                circle.enabled = false;

                rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("action") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                animator.SetBool("isHide", true);
                isHide = true;

                rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("action_loof") && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
            {
                isHide = false;
                CapsuleCollider2D capsule = gameObject.GetComponent<CapsuleCollider2D>();
                capsule.enabled = true;
                CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
                circle.enabled = true;

                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

        }

    }

    void Flip()
    {
        faceRight = !faceRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Candy")
        {
            ScoreManager.setCandy(10);
            option.isCandy = true;
            collision.gameObject.SetActive(false);
            AudioManager.Instance.Play("candy");
        }
        else if (collision.gameObject.tag == "Finish")
        {
            if (gameObject.tag == "Player2")
            {
                if (isTouch == false)
                {
                    Debug.Log("끝");
                    isTouch = true;
                    collision.enabled = false;
                    gameManager.Clear();
                    animator.SetBool("isMove", false);
                    stopManager.ScriptOFF();
                    AudioManager.instance.Stop("walk1");
                    AudioManager.instance.Stop("walk2");

                }
            }
        }
    }

}
