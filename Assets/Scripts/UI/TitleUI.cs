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
        UIManager.Instance.ShowUI("LibraryUI")
            .GetComponent<LibraryUI>()
            .Init(true);
    }

    public void SettingButtonClick()
    {
        UIManager.Instance.ShowUI("SettingUI");
    }

    public void ExitButtonClick()
    {
        Application.Quit(); 
    }
}
