using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointer : MonoBehaviour, IPointerClickHandler
{
    public LevelManager levelManager;


    public void OnPointerClick(PointerEventData eventData)
    {
        levelManager.num = int.Parse(eventData.selectedObject.name) - 1;
        levelManager.MovePiece();
    }
}
