using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMenu : MonoBehaviour
{
    public Button retry;
    public Button selectLevel;
    public Button nextLevel;

    void Start()
    {
        retry.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        selectLevel.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        nextLevel.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

}
