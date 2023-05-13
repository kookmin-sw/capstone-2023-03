using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructs;

public class Battle : MonoBehaviour
{

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {


    }

    public static void Draw()
    {
        if (BattleData.Instance.Deck.Count <= 0)
        {
            foreach (CardStruct card in BattleData.Instance.Origin_Deck)
            {
                BattleData.Instance.Deck.Add(card);
            }
            BattleData.Instance.Trash = new List<CardStruct>();
        }
        int randomIndex = Random.Range(0, BattleData.Instance.Deck.Count);
        BattleData.Instance.Hand.Add(BattleData.Instance.Deck[randomIndex]);
        BattleData.Instance.Deck.RemoveAt(randomIndex);
    }

    public static void Discard(CardStruct card)
    {
        BattleData.Instance.Trash.Add(card);
        BattleData.Instance.Hand.Remove(card);
    }

    public static void End_turn()
    {
        foreach (CardStruct card in BattleData.Instance.Hand)
        {
            BattleData.Instance.Trash.Add(card);
        }
        BattleData.Instance.Hand = new List<CardStruct>();
    }
}
