using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    Animator animator;
    bool isEnd = false;
    string backgroundMusic = "Title";

    Option option;

    private void Start()
    {
        option = FindObjectOfType<Option>();
        animator = GetComponent<Animator>();
        isEnd = false;
        option.isEnd = true;
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            if (!isEnd)
            {
                isEnd = true;
                SceneTransition();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
            SceneTransition();
    }

    void SceneTransition()
    {
        option.isEnd = false;
        AudioManager.Instance.FadeOut(backgroundMusic);
        LevelLoader.Instance.LoadLevel("Level");
    }
}
