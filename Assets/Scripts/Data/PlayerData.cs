using System.Collections.Generic;
using DataStructs;

//다른 클래스들에서는 playData에 접근해서 저장된 데이터를 사용하거나, 정보가 변화 시(혹은 세이브 시도 시) playData를 실시간으로 갱신함.
public class PlayerData : Singleton<PlayerData>
{
    public int ChannelLevel { get; set; } = 1;
    public List<CardStruct> Deck { get; set; }

    protected override void Awake()
    {
        base.Awake();

        Deck = new List<CardStruct>(){
            GameData.Instance.CardList[0],
            GameData.Instance.CardList[0], 
            GameData.Instance.CardList[0],
            GameData.Instance.CardList[1],
            GameData.Instance.CardList[1],
            GameData.Instance.CardList[1],
            GameData.Instance.CardList[2],
            GameData.Instance.CardList[3]
        };  

    }
}
