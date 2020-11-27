using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject Canvas;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Canvas.gameObject.activeSelf)
                Canvas.gameObject.SetActive(true);
            else
                Canvas.gameObject.SetActive(false);
        }
    }

    public void YesButton()
    {
        Application.Quit();
    }

    public void NoButton()
    {
        Canvas.gameObject.SetActive(false);
    }
}
