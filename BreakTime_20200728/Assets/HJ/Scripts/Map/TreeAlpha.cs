using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAlpha : MonoBehaviour
{
    SpriteRenderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            Color color = renderer.color;
            color.a = 0.35f;
            renderer.color = color;
        }
        //else if(collision.transform.CompareTag("Player2"))
        //{
        //    Color color = renderer.color;
        //    color.a = 0.35f;
        //    renderer.color = color;
        //}

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.transform.CompareTag("Player1") && collision.transform.CompareTag("Player2"))
        //{
        //    Color color = renderer.color;
        //    color.a = 0.9f;
        //    renderer.color = color;
        //}
        //else if (collision.transform.CompareTag("Player2"))
        //{
        //    Color color = renderer.color;
        //    color.a = 0.9f;
        //    renderer.color = color;
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player1"))
        {
            Color color = renderer.color;
            color.a = 0.35f;
            renderer.color = color;
        }
        else if (collision.transform.CompareTag("Player2"))
        {
            Color color = renderer.color;
            color.a = 0.35f;
            renderer.color = color;
        }
        else if (collision.transform.CompareTag("Player1") && collision.transform.CompareTag("Player2"))
        {
            Color color = renderer.color;
            color.a = 0.35f;
            renderer.color = color;
        }
        else
        {
            Color color = renderer.color;
            color.a = 0.9f;
            renderer.color = color;
        }
    }
}
