using System;
using UnityEngine;
using UnityEngine.EventSystems;

//Event System에서 발생시킬 이벤트를 구독하는 인터페이스를 한 곳에서 구현
public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    public event Action<PointerEventData> PointerEnter;
    public event Action<PointerEventData> PointerDown;
    public event Action<PointerEventData> PointerExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter?.Invoke(eventData);    
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit?.Invoke(eventData);
    }
}
