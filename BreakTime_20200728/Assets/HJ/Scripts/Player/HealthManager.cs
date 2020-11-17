using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    public int maxHealth = 1;
    public int health = 1;
    bool isDie = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Die()
    {
        isDie = true;
        rigid.velocity = Vector2.zero;

        animator.SetTrigger("Dead");
        animator.SetBool("Arrive", false);
        BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = false;
        CapsuleCollider2D capsule = gameObject.GetComponent<CapsuleCollider2D>();
        capsule.enabled = false;
        CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
        circle.enabled = false;

        Vector2 dieVelocity = new Vector2(0, 6f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.tag == "Obstacle")
        {
            health--;
            Die();
            Invoke("CollMake", 1.5f);
        }
        else if (collision.gameObject.tag == "Bottom")
        {
            health = 0;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                Attack(collision.transform);
            }
            else
            {
                health--;
                Die();
                Invoke("CollMake", 1.5f);
            }
            

        }
    }

    void Attack(Transform monster)
    {
        MonsterHealth monsterHealth = monster.GetComponent<MonsterHealth>();
        monsterHealth.Damaged();
    }

    public void CollMake()
    {
        BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = true;
        CapsuleCollider2D capsule = gameObject.GetComponent<CapsuleCollider2D>();
        capsule.enabled = true;
        CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
        circle.enabled = true;
        animator.SetBool("Arrive", true);
        health = maxHealth;
    }
}
