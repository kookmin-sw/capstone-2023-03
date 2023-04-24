using System.Collections.Generic;
using DataStructs;

//다른 클래스들에서는 playData에 접근해서 저장된 데이터를 사용하거나, 정보가 변화 시 playData를 실시간으로 갱신함.
public class PlayerData : Singleton<PlayerData>
{
    public int ChannelLevel { get; set; } //채널 레벨

    public int Viewers { get; set; } //애청자 수

    public int CurrentHp { get; set; } //현재 체력

    public int MaxHp { //최대 체력
        get 
        {
            int maxhp = 80 + (100 / Viewers);  
            return maxhp;
        }
    }

    public int Money { get; set; }  //현재 돈

    public int Energy { get; set; } //현재 에너지

    public List<CardStruct> Deck { get; set; }

    protected override void Awake()
    {
        base.Awake();

        //초기 데이터 설정

        ChannelLevel = 1;
        Viewers = 0;
        CurrentHp = MaxHp;
        Money = 0;
        Energy = 5;

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

    //레벨업 시 데이터 변경들을 한번에...
    public void ChannelLevelUp()
    {

    }

}
