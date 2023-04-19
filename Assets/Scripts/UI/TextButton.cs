using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    TMP_Text text;

    private void OnDisable()
    {
        text.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(gameObject.TryGetComponent(out Button button))
        {
            if(button.interactable)
            {
                SoundManager.Instance.Play("Sounds/ClickEffect");
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.TryGetComponent(out Button button))
        {
            if (button.interactable)
            {
                text.color = Color.gray;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.TryGetComponent(out Button button))
        {
            if (button.interactable)
            {
                text.color = Color.white;
            }
        }
    }
}
