using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "Ground" ||
    //        collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}