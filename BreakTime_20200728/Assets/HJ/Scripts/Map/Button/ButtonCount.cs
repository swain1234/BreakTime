using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCount : MonoBehaviour
{
    public int Bcount = 0;

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
        if(collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            Bcount += 1;
        }
    }
}
