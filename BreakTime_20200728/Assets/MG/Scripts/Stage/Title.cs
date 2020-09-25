using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    [SerializeField] private Image title;
    private FadeManager fadeManager;
    public bool isTitle = true;

    private void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();
    }

    private void Update()
    {
        if(Input.anyKeyDown && isTitle == true)
        {
            StartCoroutine(TitleClick());
        }
    }

    IEnumerator TitleClick()
    {
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        title.gameObject.SetActive(false);
        isTitle = false;
        fadeManager.FadeIn();
        SceneManager.LoadScene("Level");
    }
}
