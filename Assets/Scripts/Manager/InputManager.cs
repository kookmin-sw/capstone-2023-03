using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//인풋 액션들을 관리
public class InputManager : Singleton<InputManager>
{
    public GameActions KeyActions { get; set; }

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);

        KeyActions = new GameActions();
        KeyActions.Enable();
    }

    public void EnablePlayerActions(bool isActivated)
    {
        if(isActivated == true)
        {
            KeyActions.Player.Enable();
        }
        else
        {
            KeyActions.Player.Disable();
        }
    }

    public void EnableUIActions(bool isActivated)
    {
        if (isActivated == true)
        {
            KeyActions.UI.Enable();
        }
        else
        {
            KeyActions.UI.Disable();
        }
    }
}
