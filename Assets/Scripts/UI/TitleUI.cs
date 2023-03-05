using UnityEngine;


public class TitleUI : MonoBehaviour
{
    public void StartButtonClick()
    {
        SceneLoader.Instance.LoadScene("GameScene");
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
