using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 소지한 카드를 들고있는 클래스
//JSON 파일로 세이브하는 함수가 필요할 듯.

public class CardPlayerData : Singleton<CardPlayerData>
{
    public List<Card> PlayerDeck { get; set; } = new List<Card>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        JsonData dialogData = DataManager.Instance.LoadJson("PlayerDeck");
    }

    public void DiscardCard()
    {

    }
}
