using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{ 
    private void Awake()
    {
        SoundManager.Instance.Play("Sounds/Stage1Bgm", Sound.Bgm);
        StageManager.Instance.CreateMap();
        PlayerData.Instance.LoadPlayerData(); //게임 입장시마다 플레이어 데이터 초기화
        UIManager.Instance.ShowUI("StatUI");
    }
}
