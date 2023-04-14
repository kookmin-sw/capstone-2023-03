using UnityEngine;


public class TitleUI : BaseUI
{
    public void StartButtonClick()
    {
        SoundManager.Instance.Play("Sounds/ClickEffect");
        UIManager.Instance.ShowUI("LoadUI");
    }

    public void LibraryButtonClick()
    {
        SoundManager.Instance.Play("Sounds/ClickEffect");
        UIManager.Instance.ShowUI("LibraryUI")
            .GetComponent<LibraryUI>()
            .Init(true);
    }

    public void SettingButtonClick()
    {
        SoundManager.Instance.Play("Sounds/ClickEffect");
    }

    public void ExitButtonClick()
    {
        SoundManager.Instance.Play("Sounds/ClickEffect");
        Application.Quit(); 
    }
}
