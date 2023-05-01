using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DataStructs;

//��ȭâ UI Ŭ����
//1. ��ȭ�� ��ȭâ UI�� ������.
//2. ��ǲ�� ���� ��ȭâ�� ���� ��ȭ�� ����.

public class DialogUI : BaseUI, IPointerDownHandler
{
    private int dialogIndex;
    private int lineCount;
    private LineStruct currentLine;

    [SerializeField]
    private Image portrait;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text lineText;

    private Action CloseAction;

    private void OnEnable()
    {
        //�÷��̾� ���� ��Ȱ��ȭ, UI ���� Ȱ��ȭ
        //���ͷ� ��ȭâ �����ϴ� �Լ� �����ϰ� �̺�Ʈ ���
        //��ǲ�ý��ۿ� �̺�Ʈ �Լ� ����� ��, ���ٷ� ������� �ʴ� �� ���� ��. ������ �𸣰ڴµ� �ߺ� ���� ��������
        InputActions.keyActions.Player.Disable();
        InputActions.keyActions.UI.Enable();
        InputActions.keyActions.UI.Check.started += NextDialogByCheck;
    }

    private void OnDisable()
    {
        InputActions.keyActions.Player.Enable();
        InputActions.keyActions.UI.Disable();
        InputActions.keyActions.UI.Check.started -= NextDialogByCheck;
    }

    //���콺�� UI Ŭ�� �� �۵�
    public void OnPointerDown(PointerEventData eventData)
    {
        NextDialog();
    }

    //���� Ű ���� �� �۵�
    public void NextDialogByCheck(InputAction.CallbackContext context)
    {
        NextDialog();
    }

    //ó�� ��ȭâ�� ���� �� �ʱ�ȭ
    public void Init(int index, Action CloseCallback = null)
    {
        dialogIndex = index;
        CloseAction = CloseCallback;
        lineCount = 0;
        NextDialog();
    }

    //��ȭâ�� �����Ű�� �Լ�. ��ȭ ������ ��ųʸ����� ��ȭ ������ ��������, ������ ������ ���̻� ������ ��ȭâ�� �ݴ´�.
    //��ȭ ���뿡 ���� UI ������ ��Ʈ����Ʈ�� ����.
    public void NextDialog()
    {
        //���� ��ȭ�� ������ â �ݰ� ����
        if (GameData.Instance.DialogDic[dialogIndex].Count == lineCount) 
        {
            UIManager.Instance.HideUI("DialogUI");
            CloseAction?.Invoke();
            return;
        }

        //�� �� ��������
        currentLine = GameData.Instance.DialogDic[dialogIndex][lineCount];

        //�̸�, �ʻ�ȭ ���� ���� ���� �̸�, �ʻ�ȭ â�� ����
        if (currentLine.portrait == null || currentLine.portrait == "")
        {
            portrait.gameObject.SetActive(false);   
        }
        else
        {
            portrait.sprite = GameData.Instance.SpriteDic[currentLine.portrait];
        }

        if(currentLine.name == null || currentLine.portrait == "")
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
}
