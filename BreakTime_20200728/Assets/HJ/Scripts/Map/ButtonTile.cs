using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : MonoBehaviour
{
    public GameObject tile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player2"))
        {
            collision.transform.SetParent(transform);
        }
        else if (collision.transform.CompareTag("Player1"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player2"))
        {
            collision.transform.SetParent(null);
        }
        else if(collision.transform.CompareTag("Player1"))
        {
            collision.transform.SetParent(null);
        }
    }
}
