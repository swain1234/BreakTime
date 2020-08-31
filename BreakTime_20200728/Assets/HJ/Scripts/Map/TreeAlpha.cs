using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAlpha : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();     
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            Color color = spriteRenderer.color;
            color.a = 0.35f;
            spriteRenderer.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            Color color = spriteRenderer.color;
            color.a = 0.9f;
            spriteRenderer.color = color;
        }
    }
}
