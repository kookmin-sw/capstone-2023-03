using DevionGames.UIWidgets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingUI : MonoBehaviour
{

    private bool isFullScreen;

    [SerializeField]
    public TMP_Text ScreenButtonText;

    private void Awake()
    {
        // 현재 스크린 모드 가져오기
        isFullScreen = Screen.fullScreen;
        ScreenButtonText.text = isFullScreen ? "전체화면" : "창모드";
    }

    //풀스크린 여부에 따라 클릭시 화면 모드 전환하고 버튼 텍스트도 바꾸기.
    public void ScreenModeButtonClick()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
        ScreenButtonText.text =  isFullScreen ?  "전체화면" : "창모드";
    }    

    public void ExitButtonClick()
    {
        UIManager.Instance.HideUI("SettingUI");
    }
}
