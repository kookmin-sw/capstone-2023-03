using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    //소환한 모든 UI의 루트인 게임오브젝트
    public GameObject UIRoot
    {
        get
        {
            GameObject root = GameObject.Find("UIRoot");
            if (root == null)
            {
                root = new GameObject();
                root.name = "UIRoot";
            }
            return root;
        }
    }

    //팝업 UI들을 스택으로 관리
    private Stack<GameObject> UIStack = new Stack<GameObject>();


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    //ESC키로 UI 닫기.
    private void OnEnable()
    {
        InputActions.keyActions.UI.ESC.started += context => { CloseUI(); };
    }

    private void OnDisable()
    {
        InputActions.keyActions.UI.ESC.started -= context => { CloseUI(); };
    }   

    //ShowUI의 차이점은 UI 스택에 넣지 않는다는 것.
    //정확히는 UI 스택에 넣지 않을 UI 요소들은 UIElement라는 폴더에 저장하도록.

    public GameObject ShowUIElement(string name, Transform parent)
    {
        return AssetLoader.Instance.Instantiate($"Prefabs/UIElement/{name}", parent);
    }

    //일반 UI를 로드해서 화면에 띄우는 함수
    //위층의 UI가 활성화되면 최적화/겹쳐보임 방지를 위해 아래에 깔린 UI를 비활성화하는 게 기본 설정.
    public GameObject ShowUI(string name, bool hidePreviousPanel = true)
    {
        GameObject ui = AssetLoader.Instance.Instantiate($"Prefabs/UI/{name}", UIRoot.transform);

        if (UIStack.Count > 0 && hidePreviousPanel)
        {
            UIStack.Peek().SetActive(false);
        }
        UIStack.Push(ui);
        return ui;
    }

    //UI 스택에서 맨 위에 있는 UI를 제거
    //이전 UI가 숨김처리 되어있으면 다시 보여줌
    //ESC키로 범용적으로 UI를 지울때 쓰는데, 그러므로 마지막 남은 UI는 안 지워지도록 함
    public void CloseUI()
    {
        if (UIStack.Count > 1)
        {
            GameObject ui = UIStack.Pop();
            AssetLoader.Instance.Destroy(ui);
            UIStack.Peek().SetActive(true);
        }
    }

    //UI 스택에서 맨 위에 있는 특정 UI를 제거
    //이전 UI가 숨김처리 되어있으면 다시 보여줌
    //범용적으로 지우는 함수와 구분하는 이유는, 이쪽은 UI 스택이 빌 상황에서도 지우는 함수라서.
    public void ClosePanel(string name)
    {
        if (UIStack.Count > 0 && UIStack.Peek().name == name)
        {
            GameObject ui = UIStack.Pop();

            AssetLoader.Instance.Destroy(ui);
            
            if(UIStack.Count > 0)
            {
                UIStack.Peek().SetActive(true);
            }
        }
    }

    public void Clear()
    {
        UIStack.Clear();
    }
}
