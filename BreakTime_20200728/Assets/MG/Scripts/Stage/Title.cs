using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    [SerializeField] private Image title;
    Animator animator;
    string backgroundMusic = "Title";

    private void Start()
    {
        animator = GetComponent<Animator>();
        AudioManager.Instance.Play(backgroundMusic);
    }

    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            StartCoroutine(SceneTransfer());
        }
        if(Input.GetKeyDown(KeyCode.P))
            StartCoroutine(SceneTransfer());
    }

    IEnumerator SceneTransfer()
    {
        AudioManager.Instance.FadeOut(backgroundMusic);
        FadeManager.Instance.Fade();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level");
    }

    public void EffectSound()
    {
        AudioManager.Instance.Play("flipBook");
    }
}
