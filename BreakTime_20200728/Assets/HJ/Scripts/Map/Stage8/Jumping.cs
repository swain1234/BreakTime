using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    SpriteRenderer sr;
    string spriteName;
    public bool isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        spriteName = sr.sprite.name;
        if(transform.name.IndexOf("Clone") != -1)
        {
            Destroy(this.gameObject);
        }
    }

    void ChangeSprite()
    {
        sr.sprite = (Sprite)Resources.Load(spriteName + "_on", typeof(Sprite));
    }

    void BackSprite()
    {
        sr.sprite = (Sprite)Resources.Load(spriteName, typeof(Sprite));
    }

    void ChangeSpring()
    {
        Instantiate(this, transform.position, transform.rotation);
        Invoke("ChangeSprite", 0.1f);
        isTrigger = true;

    }

    void BackSpring()
    {
        Instantiate(this, transform.position, transform.rotation);
        Invoke("BackSprite", 0.5f);
        isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            ChangeSpring();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            BackSpring();
        }
    }
}
