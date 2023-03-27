using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//대화창 UI 클래스
//1. 대화를 대화창 UI에 보여줌.
//2. 인풋을 통해 대화창을 다음 대화로 진행.

public class DialogUI : BaseUI
{
    private int dialogIndex;
    private int lineCount;
    private LineData currentLine;
    private CustomButton customButton;

    [SerializeField]
    private Image portrait;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text lineText;

    private Action DialogClosed;

    private void Awake()
    {
        customButton = GetComponent<CustomButton>();
    }

    private void OnEnable()
    {
        //플레이어 조작 비활성화, UI 조작 활성화
        InputActions.keyActions.Player.Disable();
        InputActions.keyActions.UI.Enable();

        //엔터, 마우스 클릭으로 대화창 진행하는 함수 실행하게 이벤트 등록
        //인풋시스템에 이벤트 함수 등록할 때, 람다로 등록하지 않는 게 좋을 듯. 왠지는 모르겠는데 중복 실행 오류난다
        InputActions.keyActions.UI.Check.started += NextDialogByCheck;
        customButton.PointerDown += context => { NextDialog(); };
    }

    private void OnDisable()
    {
        InputActions.keyActions.Player.Enable();
        InputActions.keyActions.UI.Disable();

        InputActions.keyActions.UI.Check.started -= NextDialogByCheck;
        customButton.PointerDown -= context => { NextDialog(); };
    }

    //처음 대화창이 열릴 때 초기화
    public void Init(int index, Action CloseCallback = null)
    {
        dialogIndex = index;
        DialogClosed = CloseCallback;
        lineCount = 0;
        NextDialog();
    }

    //대화창을 진행시키는 함수. 대화 데이터 딕셔너리에서 대화 내용을 가져오고, 가져올 내용이 더이상 없으면 대화창을 닫는다.
    //대화 내용에 따라 UI 구조와 포트레이트를 변경.
    public void NextDialog()
    {
        //다음 대화가 없으면 창 닫고 종료
        if (GameDataCon.Instance.DialogDic[dialogIndex].Count == lineCount) 
        {
            DialogClosed?.Invoke();
            PanelManager.Instance.ClosePanel("DialogUI");
            return;
        }

        //한 줄 가져오기
        currentLine = GameDataCon.Instance.DialogDic[dialogIndex][lineCount];

        //이름, 초상화 등이 없는 경우는 이름, 초상화 창을 제거
        if (currentLine.portrait == null)
        {
            portrait.gameObject.SetActive(false);   
        }
        else
        {
            portrait.sprite = GameDataCon.Instance.SpriteDic[currentLine.portrait];
        }

        if(currentLine.name == null)
        {
            nameText.transform.parent.gameObject.SetActive(false);  
        }
        else
        {
            nameText.text = currentLine.name;
        }
        lineText.text = currentLine.line;

        lineCount++;
    }

    public void NextDialogByCheck(InputAction.CallbackContext context)
    {
        NextDialog();
    }

}
