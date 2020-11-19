using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    Animator animator;
    string backgroundMusic = "Title";
    bool isEnd = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        AudioManager.Instance.Play(backgroundMusic);
        isEnd = false;
    }

    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            if (!isEnd)
            {
                isEnd = true;
                SceneTransition();
            }
        }
        if(Input.GetKeyDown(KeyCode.P))
            SceneTransition();
    }

    void SceneTransition()
    {
        AudioManager.Instance.FadeOut(backgroundMusic);
        LevelLoader.Instance.LoadLevel("Level");
    }
}
