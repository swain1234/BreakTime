using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action1 : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("Attack");
            isAttack = true;
        }
    }
}
