using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    TMP_Text buttonText;

    private void OnDisable()
    {
        buttonText.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = Color.white;
    }
}
