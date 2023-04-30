using System;
using System.Collections.Generic;
using DataStructs;

//다른 클래스들에서는 playData에 접근해서 저장된 데이터를 사용하거나, 정보가 변화 시 playData를 실시간으로 갱신함.
public class PlayerData : Singleton<PlayerData>
{

    private int channelLevel;
    private int viewers;
    private int currentHp;
    private int maxHp;
    private int money;
    private int energy;

    public int ChannelLevel {
        get { return channelLevel; }
        set 
        {
            OnDataChange?.Invoke();
            channelLevel = value;
        }
    
    }  //채널 레벨

    public int Viewers {
        get { return viewers; }
        set
        {
            viewers = value;
            OnDataChange?.Invoke();
        }
    }  //애청자 수

    public int CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            OnDataChange?.Invoke();
        }
    }  //현재 체력

    public int MaxHp { //최대 체력
        get 
        {
            if (viewers == 0) return 80;
            maxHp = 80 + (viewers / 100);
            return maxHp;
        }
        set 
        {
            maxHp = value;
            OnDataChange?.Invoke();
        }
    }

    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            OnDataChange?.Invoke();
        }
    }  //현재 돈

    public int Energy
    {
        get { return energy; }
        set
        {
            energy = value;
            OnDataChange?.Invoke();
        }
    } //현재 에너지

    public List<CardStruct> Deck { get; set; }

    //데이터 변경 시 발생
    public event Action OnDataChange;

    protected override void Awake()
    {
        base.Awake();

        //초기 데이터 설정

        ChannelLevel = 1;
        Viewers = 0;
        CurrentHp = MaxHp;
        Money = 100;
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
