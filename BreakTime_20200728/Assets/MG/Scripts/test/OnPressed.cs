using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed;

    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
    }

    private void Update()
    {
        if (isPressed)
        {
            Debug.Log("11");
        }
    }
}
