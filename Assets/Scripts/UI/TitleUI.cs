using UnityEngine;


public class TitleUI : BaseUI
{
    public void StartButtonClick()
    {
        PanelManager.Instance.ShowPanel("LoadUI");
    }

    public void LibraryButtonClick()
    {
        LibraryUI libraryUI = PanelManager.Instance.ShowPanel("LibraryUI").GetComponent<LibraryUI>();
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
