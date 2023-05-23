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

    public static bool Draw()
    {
        
        if (BattleData.Instance.Deck.Count <= 0)
        {
            if(BattleData.Instance.Trash.Count <= 0)
            {
                return false;
            }
            else
            {
                foreach (CardStruct card in BattleData.Instance.Trash)
                {
                    BattleData.Instance.Deck.Add(card);
                }
                BattleData.Instance.Trash = new List<CardStruct>();
            }
        }

        if (BattleData.Instance.Hand.Count >= BattleData.Instance.MaxHand)
        {
            UIManager.Instance.ShowUI("LibraryUI").GetComponent<LibraryUI>().Init(LibraryMode.Battle_Trash_Hand);
        }
        int randomIndex = Random.Range(0, BattleData.Instance.Deck.Count);
        BattleData.Instance.Hand.Add(BattleData.Instance.Deck[randomIndex]);
        BattleData.Instance.Deck.RemoveAt(randomIndex);
        return true;
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
        BattleData.Instance.CurrentEnergy = BattleData.Instance.MaxEnergy;
        BattleData.Instance.Shield = 0;

    }

    public static void Start_turn()
    {
        BattleData.Instance.CurrentEnergy = BattleData.Instance.MaxEnergy;
        BattleData.Instance.CurrentTurn++;
        for(int i = 0; i < 3; i++)
        {
            Debug.Log(EnemyData.Instance.Isalive[i]+"/"+i.ToString());
            if (EnemyData.Instance.Isalive[i])
            {
                EnemyData.Instance.SetPat(i);
            }
            else
            {
                EnemyData.Instance.Pat[i] = 0;
            }
            EnemyData.Instance.SetPat(i);
        }
    }

    public static void ChangeCurrentHealth(float value)
    {
        BattleData.Instance.CurrentHealth += value;

        if (BattleData.Instance.CurrentHealth <= 0)
        {
            BattleData.Instance.IsAlive = false;
        }
        if (BattleData.Instance.CurrentHealth > BattleData.Instance.MaximumHealth)
        {
            BattleData.Instance.CurrentHealth = BattleData.Instance.MaximumHealth;
        }
    }

    public static void UseCard(CardStruct card)
    {
        BattleData.Instance.CurrentEnergy -= card.cost;
        BattleData.Instance.Trash.Add(card);
        BattleData.Instance.Hand.Remove(card);
        
    }

    public static void ChangeEnemyHealth(int num, float value)
    {
        EnemyData.Instance.CurrentHP[num] += value;
        if (EnemyData.Instance.CurrentHP[num] <= 0)
        {
            EnemyData.Instance.Isalive[num] = false;
        }
        if (EnemyData.Instance.CurrentHP[num] > EnemyData.Instance.MaxHP[num])
        {
            EnemyData.Instance.CurrentHP[num] = EnemyData.Instance.MaxHP[num];
        }
    }
}
