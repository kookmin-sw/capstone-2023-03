using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{ 
    private void Awake()
    {
        SoundManager.Instance.Play("Sounds/Stage1Bgm", Sound.Bgm);
        StageManager.Instance.CreateMap();
        UIManager.Instance.ShowUI("StatUI");
    }
}
