using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    Animator animator;
    bool isEnd = false;
    string backgroundMusic = "Title";

    private void Start()
    {
        animator = GetComponent<Animator>();
        isEnd = false;
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
        AudioManager.Instance.FadeOut(backgroundMusic);
        LevelLoader.Instance.LoadLevel("Level");
    }
}
