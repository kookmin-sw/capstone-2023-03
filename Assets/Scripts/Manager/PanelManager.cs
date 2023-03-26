using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PanelManager : Singleton<PanelManager>
{
    //소환한 모든 UI의 루트인 게임오브젝트
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

    //팝업 UI들을 스택으로 관리
    private Stack<BaseUI> panelStack = new Stack<BaseUI>();


    protected override void Awake()
    {
        base.Awake();
    }

    //ESC키로 UI 닫기.
    private void OnEnable()
    {
        InputActions.keyActions.UI.ESC.started += context => { ClosePanel(); };
        SceneManager.sceneLoaded += (con1, con2) => { panelStack.Clear(); };
    }

    private void OnDisable()
    {
        InputActions.keyActions.UI.ESC.started -= context => { ClosePanel(); };
        SceneManager.sceneLoaded -= (con1, con2) => { panelStack.Clear(); };
    }   

    //일반 UI를 로드해서 화면에 띄우는 함수
    //위층의 UI가 활성화되면 최적화/겹쳐보임 방지를 위해 아래에 깔린 UI를 비활성화
    public BaseUI ShowPanel(string name, Action CloseCallback = null, bool hidePreviousPanel = true)
    {
        BaseUI panel = AssetLoader.Instance.Instantiate($"Prefabs/UI/{name}", PanelRoot.transform).GetComponent<BaseUI>();

        if(CloseCallback != null) panel.PanelClosed += CloseCallback;

        if (panelStack.Count > 0 && hidePreviousPanel)
        {
            panelStack.Peek().gameObject.SetActive(false);
        }
        panelStack.Push(panel);
        return panel;
    }

    //UI 스택에서 맨 위에 있는 UI를 제거
    //이전 UI가 숨김처리 되어있으면 다시 보여줌
    //ESC키로 범용적으로 UI를 지울때 쓰는데, 그러므로 마지막 남은 UI는 안 지워지도록 함
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

    //UI 스택에서 맨 위에 있는 특정 UI를 제거
    //이전 UI가 숨김처리 되어있으면 다시 보여줌
    //범용적으로 지우는 함수와 구분하는 이유는, 이쪽은 UI 스택이 빌 상황에서도 지우는 함수라서.
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
}
