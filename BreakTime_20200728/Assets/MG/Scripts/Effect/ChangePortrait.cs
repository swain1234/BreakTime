using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePortrait : MonoBehaviour
{
    [SerializeField] Image lib;
    [SerializeField] Image red;

    bool isLib = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isLib)
            {
                lib.gameObject.SetActive(false);
                red.gameObject.SetActive(true);
                isLib = false;
            }
            else
            {
                lib.gameObject.SetActive(true);
                red.gameObject.SetActive(false);
                isLib = true;
            }
        }

    }

}
