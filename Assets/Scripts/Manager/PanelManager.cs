using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanelManager : Singleton<PanelManager>
{
    //��ȯ�� ��� UI�� ��Ʈ�� ���ӿ�����Ʈ
    public GameObject PanelRoot
    {
        get
        {
            GameObject root = GameObject.Find("PanelRoot");
            if (root == null)
            {
                root = new GameObject();
                root.name = "PanelRoot";
            }
            return root;
        }
    }

    //�˾� UI���� �������� ����
    private Stack<BaseUI> panelStack = new Stack<BaseUI>();


    protected override void Awake()
    {
        base.Awake();
    }

    //ESCŰ�� UI �ݱ�.
    private void OnEnable()
    {
        InputManager.Instance.KeyActions.UI.ESC.started += context => { ClosePanel(); };
    }

    private void OnDisable()
    {
        InputManager.Instance.KeyActions.UI.ESC.started -= context => { ClosePanel(); };
    }

    //�Ϲ� UI�� �ε��ؼ� ȭ�鿡 ���� �Լ�
    //������ UI�� Ȱ��ȭ�Ǹ� ����ȭ/���ĺ��� ������ ���� �Ʒ��� �� UI�� ��Ȱ��ȭ
    public BaseUI ShowPanel(string name, bool hidePreviousPanel = true, Action CloseCallback = null)
    {
        BaseUI panel = AssetLoader.Instance.Instantiate($"Prefabs/UI/{name}", PanelRoot.transform).GetComponent<BaseUI>();

        panel.PanelClosed += CloseCallback;

        if (panelStack.Count > 0 && hidePreviousPanel)
        {
            panelStack.Peek().gameObject.SetActive(false);
        }
        panelStack.Push(panel);
        return panel;
    }

    //UI ���ÿ��� �� ���� �ִ� UI�� ����
    //���� UI�� ����ó�� �Ǿ������� �ٽ� ������
    //ESCŰ�� ���������� UI�� ���ﶧ�� ���µ�, �׷��Ƿ� ������ ���� UI�� �� ���������� ��
    public void ClosePanel()
    {
        if (panelStack.Count > 1)
        {
            BaseUI panel = panelStack.Pop();
            panel.PanelClosed?.Invoke();

            AssetLoader.Instance.Destroy(panel.gameObject);
            panelStack.Peek().gameObject.SetActive(true);
        }
    }

    //UI ���ÿ��� �� ���� �ִ� Ư�� UI�� ����
    //���� UI�� ����ó�� �Ǿ������� �ٽ� ������
    //���������� ����� �Լ��� �����ϴ� ������, ������ UI ������ �� ��Ȳ������ ����� �Լ���.
    public void ClosePanel(string name)
    {
        if (panelStack.Count > 0 && panelStack.Peek().name == name)
        {
            BaseUI panel = panelStack.Pop();
            panel.PanelClosed?.Invoke();

            AssetLoader.Instance.Destroy(panel.gameObject);
            
            if(panelStack.Count > 0)
            {
                panelStack.Peek().gameObject.SetActive(true);
            }
        }
    }

    //�� �Ѿ �� ���� ����
    public void Clear()
    {
        panelStack.Clear();  
    }
}