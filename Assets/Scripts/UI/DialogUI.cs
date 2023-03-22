using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    private int dialogIndex;
    private int currentLine;
    private Dialog dialog;
    private ButtonEvents buttonEvents;

    private Text nameText;
    private Text lineText;


    private void Awake()
    {
        buttonEvents = GetComponent<ButtonEvents>();
    }

    private void OnEnable()
    {
        currentLine = 0;

        //플레이어 조작 비활성화
        InputManager.Instance.KeyActions.Player.Disable();

        //엔터, 마우스 클릭으로 대화창 진행하는 함수 실행하게 이벤트 등록
        //인풋시스템에 이벤트 함수 등록할 때, 람다로 등록하지 않는 게 좋을 듯. 왠지는 모르겠는데 중복 실행 오류난다
        InputManager.Instance.KeyActions.UI.Check.started += ProgressDialogByCheck;
        buttonEvents.PointerDown += context => { ProgressDialog(); };
    }

    private void OnDisable()
    {
        //이벤트 해제, 플레이어 조작 활성화
        InputManager.Instance.KeyActions.Player.Enable();
        InputManager.Instance.KeyActions.UI.Check.started -= ProgressDialogByCheck;
        buttonEvents.PointerDown -= context => { ProgressDialog(); };
    }

    //처음 대화창을 여는 함수
    public void ShowDialog(int index)
    {
        dialogIndex = index;
        currentLine = 0;
        ProgressDialog();
    }

    //대화창을 진행시키는 함수. JSON에서 대화 내용을 가져오고, 더이상 없으면 대화창을 닫는다.
    //카운터를 올려서 다음에는 다음 대화 내용이 출력되도록 함.
    //대화 내용에 따라 UI와 포트레이트를 변경한다.
    public void ProgressDialog()
    {
        Dialog currentDialog = DialogManager.Instance.GetLine(dialogIndex, currentLine);
        if (currentDialog == null) 
        {
            Debug.Log("대화 종료");
            PanelManager.Instance.ClosePanel("DialogUI");
            return;
        }
        Debug.Log(currentDialog.name);
        Debug.Log(currentDialog.line);

        currentLine++;
    }

    public void ProgressDialogByCheck(InputAction.CallbackContext context)
    {
        ProgressDialog();
    }

}
