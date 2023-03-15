using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    //UI들을 스택으로 관리
    private Stack<GameObject> panelStack = new Stack<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    //ESC키로 UI 닫기. 일단 맨 마지막 UI는 못 닫게 해놓음
    private void OnEnable()
    {
        InputManager.Instance.KeyActions.UI.ESC.started += context => { 
            if (panelStack.Count == 1) panelStack.Peek().SetActive(!panelStack.Peek().activeSelf);  
            if (panelStack.Count > 1) HideLastPanel(); 
        };
    }

    private void OnDisable()
    {
        InputManager.Instance.KeyActions.UI.Disable();
        InputManager.Instance.KeyActions.Player.Enable();

        InputManager.Instance.KeyActions.UI.ESC.started += context => {
            if (panelStack.Count == 1) panelStack.Peek().SetActive(!panelStack.Peek().activeSelf);
            if (panelStack.Count > 1) HideLastPanel();
        };
    }

    //특정 UI를 로드해서 화면에 띄우는 함수
    //UI 스택에 추가하지 않으므로 HideLastPanel로 닫히지 않는다
    public GameObject ShowPanel(string name)
    {
        return AssetLoader.Instance.Instantiate($"Prefabs/UI/{name}", PanelRoot.transform);
    }

    //특정 UI를 로드해서 화면에 띄우고 UI 스택에 추가하는 함수.
    //hidePreviousPanel이 true이면 이전 UI를 숨김 처리
    //UI를 보여줄 때마다 매번 굳이 다시 로드해야 하는 문제가... 오브젝트 풀링은 나중에 하기로
    public void ShowPanelOnStack(string name, bool hidePreviousPanel = false)
    {
        if(hidePreviousPanel && panelStack.Count > 0)
        { 
            panelStack.Peek().SetActive(false);
        }

        GameObject panel = ShowPanel(name);

        if(panel != null)
        { 
            panelStack.Push(panel); 
        }
    }

    //UI 스택에서 맨 위에 있는 UI를 제거
    //아니면 이름으로 제거하도록 할까? 그냥 배열로 관리하고?
    //맨뒤에 있는거 제거 버전이랑 이름으로 제거 버전을 만들어서...
    //이전 UI가 숨김처리 되어있으면 다시 보여줌
    public void HideLastPanel()
    {
        if (panelStack.Count > 0)
        {
            GameObject panel = panelStack.Pop();
            AssetLoader.Instance.Destroy(panel);

            if (panelStack.Count > 0) 
            {
                panelStack.Peek().SetActive(true);    
            }
        }
    }
}
