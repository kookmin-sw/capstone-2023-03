using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUI : BaseUI
{
    public void DataButtonClick()
    {
        SceneLoader.Instance.LoadScene("GameScene");
    }
}
