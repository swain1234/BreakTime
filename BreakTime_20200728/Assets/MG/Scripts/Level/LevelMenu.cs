using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button level1;
    public Button level6;
    public Button level8;
    public Button level10;
    public Button play;
    public Button previous;
    public Button next;
    public Button close;

    private void Start()
    {
        level1.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level6.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level8.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        level10.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        play.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        previous.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        next.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        close.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}
