using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayBlack : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RedHood()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.tag == "Black")
        {
            if(collision.transform.CompareTag("Player2"))
            {

            }
        }
    }
}
