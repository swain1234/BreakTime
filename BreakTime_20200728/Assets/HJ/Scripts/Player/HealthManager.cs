using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //P_Move player;
    FadeManager fadeManager;
    Rigidbody2D rigid;
    Animator animator;
    GameManager gameManager;
    public int maxHealth = 1;
    int health = 1;
    bool isDie = false;

    // Start is called before the first frame update

    private void Awake()
    {
        //player.GetComponent<P_Move>();
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fadeManager = FindObjectOfType<FadeManager>();
        gameManager = FindObjectOfType<GameManager>();
        health = maxHealth;
    }

    void Die()
    {
        StartCoroutine(PlayerDie());
    }

    IEnumerator PlayerDie()
    {
        isDie = true;
        rigid.velocity = Vector2.zero;

        animator.SetTrigger("Dead");
        animator.SetBool("Arrive", false);
        BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = false;
        CapsuleCollider2D capsule = gameObject.GetComponent<CapsuleCollider2D>();
        capsule.enabled = false;
        if (gameObject.tag == "Player2")
        {
            CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
            circle.enabled = false;
        }

        //player.isStop = true;

        Vector2 dieVelocity = new Vector2(0, 6f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);


        yield return new WaitForSeconds(0.5f);
        //Invoke("RestartStage", 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
    }

    void CollMake()
    {
        BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = true;
        CapsuleCollider2D capsule = gameObject.GetComponent<CapsuleCollider2D>();
        capsule.enabled = true;
        if (gameObject.tag == "Player2")
        {
            CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
            circle.enabled = true;
        }
        animator.SetBool("Arrive", true);
        health = maxHealth;
    }


    void RestartStage()
    {
        gameManager.StartPosition();
    }
}
