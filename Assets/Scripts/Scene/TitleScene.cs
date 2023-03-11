using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    private void Awake()
    {
        PanelManager.Instance.ShowPanel("BG");
        PanelManager.Instance.ShowPanelOnStack("TitleUI");
    }
}
