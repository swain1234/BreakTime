using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    SpriteRenderer sr;
    string spriteName;
    public bool isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        spriteName = sr.sprite.name;
        if (transform.name.IndexOf("Clone") != -1)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeSprite()
    {
        sr.sprite = (Sprite)Resources.Load("Button/" + spriteName + "_on", typeof(Sprite));
    }

    void BackSprite()
    {
        sr.sprite = (Sprite)Resources.Load("Button/" + spriteName, typeof(Sprite));
    }

    // 켜진 버튼으로 바뀜
    void ChangeButton()
    {
        Instantiate(this, transform.position, transform.rotation);
        ChangeSprite();
        isTrigger = true;
    }
    // 꺼진 버튼으로 되돌림
    void BackButton()
    {
        Instantiate(this, transform.position, transform.rotation);
        BackSprite();
        isTrigger = false;
    }


    // 버튼에 플레이어가 충돌했을 때 켜진 버튼으로 바꿈
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            ChangeButton();
        }
    }

    // 충돌이 끝나면 원래 버튼으로 바꿈
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            BackButton();
        }
    }
}
