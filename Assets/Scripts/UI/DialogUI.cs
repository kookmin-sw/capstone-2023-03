using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    private ButtonEvents buttonEvents;

    private void Awake()
    {
        buttonEvents = GetComponent<ButtonEvents>();
    }

    private void OnEnable()
    {
        InputManager.Instance.KeyActions.Player.Disable();
        buttonEvents.PointerDown += context => { CloseDialog(); };
    }

    private void OnDisable()
    {
        InputManager.Instance.KeyActions.Player.Enable();
        buttonEvents.PointerDown -= context => { CloseDialog(); };
    }

    private void CloseDialog()
    {
        PanelManager.Instance.ClosePanel(gameObject.name);
    }
}
