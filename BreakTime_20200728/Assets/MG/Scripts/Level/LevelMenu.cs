using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    public Button level6;
    public Button level7;
    public Button level8;
    public Button level9;
    public Button level10;
    public Button button;

    private void Start()
    {
        level1.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level2.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level3.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level4.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level5.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level6.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level7.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level8.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level9.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level10.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        button.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}
