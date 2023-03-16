using UnityEngine;


public class TitleUI : BaseUI
{
    public void StartButtonClick()
    {
        PanelManager.Instance.ShowPanel("LoadUI");
    }

    public void LibraryButtonClick()
    {

    }

    public void SettingButtonClick()
    {

    }

    public void ExitButtonClick()
    {
        Application.Quit(); 
    }
}
