using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.ShowPopUpUI("TitleUI");
        SoundManager.Instance.Play("Sounds/TitleBgm", Sound.Bgm);
    }
}
