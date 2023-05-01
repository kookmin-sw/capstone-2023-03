using System;
using System.Collections.Generic;
using DataStructs;

//�ٸ� Ŭ�����鿡���� playData�� �����ؼ� ����� �����͸� ����ϰų�, ������ ��ȭ �� playData�� �ǽð����� ������.
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
    
    }  //ä�� ����

    public int Viewers {
        get { return viewers; }
        set
        {
            viewers = value;
            OnDataChange?.Invoke();
        }
    }  //��û�� ��

    public int CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            OnDataChange?.Invoke();
        }
    }  //���� ü��

    public int MaxHp { //�ִ� ü��
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
    }  //���� ��

    public int Energy
    {
        get { return energy; }
        set
        {
            energy = value;
            OnDataChange?.Invoke();
        }
    } //���� ������

    public List<CardStruct> Deck { get; set; }

    //������ ���� �� �߻���ų �̺�Ʈ
    public event Action OnDataChange;

    protected override void Awake()
    {
        base.Awake();

        //�ʱ� ������ ����

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

    //������ �� ������ ������� �ѹ���...
    public void ChannelLevelUp()
    {

    }

}
