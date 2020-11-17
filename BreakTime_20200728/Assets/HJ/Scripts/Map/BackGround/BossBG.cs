using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBG : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] bg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        Vector3 nextPos = Vector3.left * speed * Time.deltaTime;
        transform.position = currentPos + nextPos;
    }
}
