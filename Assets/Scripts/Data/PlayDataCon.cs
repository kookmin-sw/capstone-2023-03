using LitJson;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;

//플레이어 스탯
[System.Serializable]
public class PlayerStats
{
    public string name;
    public int level;
    public int positionX;
    public int positionY;
}

//플레이어 덱
[System.Serializable]
public class PlayerCard
{
    public int cardIndex;
    public int count;
}

//방 위치 데이터
public class RoomInfo
{
    public bool isCleared;
    public Define.EventType type;
    public int positionX;
    public int positionY;
}

//플레이어가 저장하고 로드해야 하는 데이터들. 스탯, 덱, 위치, 맵 등
[System.Serializable]
public class PlayData
{
    public PlayerStats playerStats;
    public List<PlayerCard> deck;
}

//데이터를 불러와서 playData에 저장해둠. 혹은 게임을 저장할 시 playData를 json파일로 저장하는 역할.
//다른 클래스들에서는 playData에 접근해서 저장된 데이터를 사용하거나, 정보가 변화 시(혹은 세이브 시도 시) playData를 실시간으로 갱신함.
public class PlayDataCon : Singleton<PlayDataCon>
{
    public PlayData PlayData { get; set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        LoadPlayData();
        SavePlayData();
    }

    public void SavePlayData()
    {
        //한글이 깨져보이고, 한줄로 출력 되는 문제가... 기능은 정상적임
        string jsonData = JsonMapper.ToJson(PlayData);
        File.WriteAllText($"Assets/Resources/Data/PlayData.json", jsonData);
    }

    public void LoadPlayData()
    {
        string filePath = $"Assets/Resources/Data/PlayData.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            PlayData = JsonMapper.ToObject<PlayData>(jsonData);
        }
        else
        {
            Debug.Log("저장된 세이브 데이터 없음!");
        }
    }
}
