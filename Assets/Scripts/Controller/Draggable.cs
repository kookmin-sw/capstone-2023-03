using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera BattleCamera;
    RectTransform rectTransform;

    public float fixedZValue = 43.25f;

    public void Start()
    {
        BattleCamera = GameObject.Find("BattleCameraParent").transform.GetChild(0).GetComponent<Camera>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    //드래그할 때 마우스를 따라 움직임

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, fixedZValue);
        transform.position = BattleCamera.ScreenToWorldPoint(mousePos);
    }

    public void OnEndDrag(PointerEventData eventData) 
    {

    }
}
