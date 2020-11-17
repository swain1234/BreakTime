using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D collider;
    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damaged()
    {
        // 죽을 때 뒤집어짐
        renderer.flipY = true;
        // 콜라이더 비활성화
        collider.enabled = false;
        // 죽을 때 살짝 점프했다가 떨어짐
        rigid.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        // 소멸
        Invoke("DeActive", 2);
    }

    // 죽은 몬스터 없애기
    void DeActive()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Attack"))
        {
            // 죽을 때 뒤집어짐
            renderer.flipY = true;
            // 콜라이더 비활성화
            collider.enabled = false;
            // 소멸
            Invoke("DeActive", 2);
        }
    }
}
