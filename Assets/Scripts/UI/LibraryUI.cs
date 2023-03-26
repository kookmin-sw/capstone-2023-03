using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LibraryUI : BaseUI
{
    // Start is called before the first frame update
    public void Awake()
    {
        
    }

    private void OnEnable()
    {
        //플레이어 조작 비활성화, UI 조작 활성화
        InputActions.keyActions.Player.Disable();
        InputActions.keyActions.UI.Enable();

        InputActions.keyActions.UI.Menu.started += Close;
    }

    // Update is called once per frame
    private void OnDisable()
    {
        InputActions.keyActions.Player.Enable();
        InputActions.keyActions.UI.Disable();

        InputActions.keyActions.UI.Menu.started -= Close;
    }

    public void ShowPlayerCards()
    {
        //현재 플레이어에 덱 리스트에 접근
        //리스트를 순회하며, 카드UI의 show card를 하면 될듯.
    }

    public void ShowAllCards()
    {
        //게임 데이터의 전체 카드 리스트에 접근
        //리스트를 순회하며, 카드UI의 show card를 하면 될듯.
    }
    private void Close(InputAction.CallbackContext context)
    {
        PanelManager.Instance.ClosePanel("LibraryUI");
    }
}
