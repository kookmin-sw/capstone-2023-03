using System.Collections;
using System.Collections.Generic;
using DataStructs;
using UnityEngine;

public class BattleData : Singleton<BattleData>
{
    public List<CardStruct> Origin_Deck = new List<CardStruct>();
    public List<CardStruct> Deck = new List<CardStruct>();
    public List<CardStruct> Hand = new List<CardStruct>();
    public List<CardStruct> Trash = new List<CardStruct>();
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        if (GameObject.Find("PlayerData") != null)
        {
            PlayerData playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
            Deck = playerData.Deck;
            Origin_Deck = Deck;
        }
        else
        {
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

        Trash = new List<CardStruct>(){
            GameData.Instance.CardList[0]
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
