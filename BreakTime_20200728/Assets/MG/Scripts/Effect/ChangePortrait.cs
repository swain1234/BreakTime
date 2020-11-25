using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePortrait : MonoBehaviour
{
    [SerializeField] Image lib;
    [SerializeField] Image red;

    bool isLib = true;
    Option option;
    DialogueManager dialogue;
    AutoFlip flip;

    private void Start()
    {
        option = FindObjectOfType<Option>();
        dialogue = FindObjectOfType<DialogueManager>();
        flip = FindObjectOfType<AutoFlip>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            ChangeImage();
    }

    void ChangeImage()
    {
        if (!option.isActive && !dialogue.transform.GetChild(0).gameObject.activeSelf &&
            !flip.transform.GetChild(0).gameObject.activeSelf)
        {
            if (isLib)
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
