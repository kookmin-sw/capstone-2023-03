using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//게임 창에서 단축키로 UI 띄우기
public class InGameUI : MonoBehaviour
{

    private void OnEnable()
    {
        UIManager.Instance.UIChange += ChangeUIControll;
        InputActions.keyActions.Player.Menu.started += OnMenuStarted;
    }

    private void OnDisable()
    {
        UIManager.Instance.UIChange -= ChangeUIControll;
        InputActions.keyActions.Player.Menu.started -= OnMenuStarted;

    }

    private void ChangeUIControll(GameObject currentUI)
    {
        if (currentUI?.name == "StatUI") //HUD 떠있을때, 즉 플레이어 조작 중에는 UI 조작키 비활성화
        {
            InputActions.keyActions.UI.Disable();
            InputActions.keyActions.Player.Enable();
        }
        else
        {
            InputActions.keyActions.Player.Disable();
            InputActions.keyActions.UI.Enable();
        }
    }

    //I버튼으로 인벤토리 UI 열기
    public void OnMenuStarted(InputAction.CallbackContext context)
    {
        LibraryUI libraryUI = UIManager.Instance.ShowUI("LibraryUI").GetComponent<LibraryUI>();
        libraryUI.Init(false);
    }
}
