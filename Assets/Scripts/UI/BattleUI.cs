using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : BaseUI
{
    // Start is called before the first frame update
    void Start()
    {
        //Canvas의 카메라를 BattleCamera로 설정, 그런 카메라가 없다면 메인 카메라로 설정
        Canvas canvas = GetComponent<Canvas>();
        Camera battleCamera = GameObject.Find("BattleCamera").GetComponent<Camera>();
        Camera mainCamera = Camera.main;
        if (battleCamera != null)
        {
            canvas.worldCamera = battleCamera;           
        }
        else
        {
            canvas.worldCamera = mainCamera;
        }
    }

    public void TrashClick()
    {
        UIManager.Instance.ShowUI("LibraryUI")
            .GetComponent<LibraryUI>()
            .Init(LibraryMode.Battle_Trash);
    }

    public void DeckClick()
    {
        UIManager.Instance.ShowUI("LibraryUI")
            .GetComponent<LibraryUI>()
            .Init(LibraryMode.Battle_Deck);
    }
}
