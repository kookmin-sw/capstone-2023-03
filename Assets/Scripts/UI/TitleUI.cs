using TMPro;
using UnityEngine;


public class TitleUI : BaseUI
{
    public void StartButtonClick()
    {
        SceneLoader.Instance.LoadScene("GameScene");
    }

    public void LibraryButtonClick()
    {
        UIManager.Instance.ShowPopUpUI("LibraryUI")
            .GetComponent<LibraryUI>()
            .Init(true);
    }

    public void SettingButtonClick()
    {
    }

    public void ExitButtonClick()
    {
        Application.Quit(); 
    }
}
