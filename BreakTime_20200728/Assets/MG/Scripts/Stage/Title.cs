using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    [SerializeField] private Image title;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            StartCoroutine(TitleClick());
        }
        if(Input.GetKeyDown(KeyCode.P))
            StartCoroutine(TitleClick());
    }

    IEnumerator TitleClick()
    {
        FadeManager.Instance.Fade();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level");
    }
}
