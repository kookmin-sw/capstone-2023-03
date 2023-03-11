using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUI : MonoBehaviour
{
    public void DataButtonClick()
    {
        SceneLoader.Instance.LoadScene("GameScene");
    }
}
