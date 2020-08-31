﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DeadCam : MonoBehaviour
{
    CinemachineVirtualCamera cine;
    Rigidbody2D rigid;
    Animator animator;
    public int maxHealth = 1;
    int health = 1;
    bool isDie = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cine = GetComponent<CinemachineVirtualCamera>();
        
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        // 현재 체력 체크
        if (health == 0)
        {
            if (!isDie)
            {
                Die();
                cine.Follow = null;
            }

            return;
        }
    }
    
    void Die()
    {
        isDie = true;
        rigid.velocity = Vector2.zero;

        animator.SetTrigger("Dead");
        BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = false;
        CapsuleCollider2D capsule = gameObject.GetComponent<CapsuleCollider2D>();
        capsule.enabled = false;

        Vector2 dieVelocity = new Vector2(0, 6f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            health--;
        }
        else if (collision.gameObject.tag == "Bottom")
        {
            health = 0;
        }
    }
}