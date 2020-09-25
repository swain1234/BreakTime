using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dissolve : MonoBehaviour
{
    Material material;

    bool isDissolving = false;
    float fade = 1f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isDissolving = true;
        if(isDissolving)
        {
            fade -= Time.deltaTime;

            if(fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
            }
            material.SetFloat("_Fade", fade);
        }
    }

}
