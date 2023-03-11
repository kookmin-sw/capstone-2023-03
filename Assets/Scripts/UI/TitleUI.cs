using UnityEngine;


public class TitleUI : BaseUI
{
    public void StartButtonClick()
    {
        PanelManager.Instance.ShowPanelOnStack("LoadUI", true);
    }

    public void LibraryButtonClick()
    {

    }

    public void SettingButtonClick()
    {

    }

    public void ExitButtonClick()
    {
        PanelManager.Instance.HideLastPanel();
    }
}
