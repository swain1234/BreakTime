using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager2 : MonoBehaviour
{
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
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Gray" || 
            collision.gameObject.tag == "Black" || collision.gameObject.tag == "Thron")
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
            health--;
            Die();
            Invoke("CollMake", 1.5f);
        }
    }


    void CollMake()
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
