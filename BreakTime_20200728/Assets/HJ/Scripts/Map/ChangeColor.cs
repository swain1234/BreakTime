using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeColor : MonoBehaviour
{
    public Tilemap tile;

    // Start is called before the first frame update
    void Start()
    {
        tile = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player1"))
        {
            tile.color = new Color(1f, 1f, 1f);
        }
    }
}
