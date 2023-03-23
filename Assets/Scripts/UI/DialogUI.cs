using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogUI : BaseUI
{
    private int dialogIndex;
    private int currentLine;
    private Dialog dialog;
    private ButtonEvents buttonEvents;

    [SerializeField]
    private Image portrait;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text lineText;


    private void Awake()
    {
        buttonEvents = GetComponent<ButtonEvents>();
    }

    private void OnEnable()
    {
        //플레이어 조작 비활성화, UI 조작 활성화
        InputManager.Instance.KeyActions.Player.Disable();
        InputManager.Instance.KeyActions.UI.Enable();

        //엔터, 마우스 클릭으로 대화창 진행하는 함수 실행하게 이벤트 등록
        //인풋시스템에 이벤트 함수 등록할 때, 람다로 등록하지 않는 게 좋을 듯. 왠지는 모르겠는데 중복 실행 오류난다
        InputManager.Instance.KeyActions.UI.Check.started += ProgressDialogByCheck;
        buttonEvents.PointerDown += context => { NextDialog(); };
    }

    private void OnDisable()
    {
        InputManager.Instance.KeyActions.Player.Enable();
        InputManager.Instance.KeyActions.UI.Disable();

        InputManager.Instance.KeyActions.UI.Check.started -= ProgressDialogByCheck;
        buttonEvents.PointerDown -= context => { NextDialog(); };
    }

    //처음 대화창이 열릴 때 쓰는 함수
    public void FirstDialog(int index)
    {
        dialogIndex = index;
        currentLine = 0;
        NextDialog();
    }

    //대화창을 진행시키는 함수. JSON에서 대화 내용을 가져오고, 가져올 내용이 더이상 없으면 대화창을 닫는다.
    //대화 내용에 따라 UI 구조와 포트레이트를 변경.
    public void NextDialog()
    {
        //한 줄 가져오기
        dialog = DialogManager.Instance.GetLine(dialogIndex, currentLine);

        //다음 대화가 없으면 창 닫고 종료
        if (dialog == null) 
        {
            PanelManager.Instance.ClosePanel("DialogUI");
            return;
        }
        
        //이름, 초상화 등이 없는 경우는 이름, 초상화 창을 제거
        if(dialog.portrait == null)
        {
            portrait.gameObject.SetActive(false);   
        }
        else
        {
            portrait.sprite = dialog.portrait;
        }

        if(dialog.name == null)
        {
            nameText.transform.parent.gameObject.SetActive(false);  
        }
        else
        {
            nameText.text = dialog.name;
        }
        lineText.text = dialog.line;

        currentLine++;
    }

    public void ProgressDialogByCheck(InputAction.CallbackContext context)
    {
        NextDialog();
    }

}
