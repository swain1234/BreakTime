using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public GameObject cannonBoll;
    public int speed = 6;

    public bool isDelay;
    public float delay = 3f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDelay)
        {
            isDelay = true;
            StartCoroutine(ShotDelay());
        }

    }
    
    IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(delay);
        isDelay = false;
        Shot();
    }

    void Shot()
    {
        GameObject bullet = Instantiate(cannonBoll, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
    }
}
