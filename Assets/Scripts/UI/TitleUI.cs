using UnityEngine;


public class TitleUI : BaseUI
{
    public void StartButtonClick()
    {
        UIManager.Instance.ShowUI("LoadUI");
    }

    public void LibraryButtonClick()
    {
        LibraryUI libraryUI = UIManager.Instance.ShowUI("LibraryUI").GetComponent<LibraryUI>();
        libraryUI.Init(true);
    }

    public void SettingButtonClick()
    {

    }

    public void ExitButtonClick()
    {
        Application.Quit(); 
    }
}
