using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    [SerializeField] private Image title;
    public bool isTitle = true;

    private void Update()
    {
        if(Input.anyKeyDown && isTitle == true)
        {
            StartCoroutine(TitleClick());
        }
    }

    IEnumerator TitleClick()
    {
        FadeManager.Instance.Fade();
        yield return new WaitForSeconds(1f);
        title.gameObject.SetActive(false);
        isTitle = false;
        SceneManager.LoadScene("Level");
    }
}
