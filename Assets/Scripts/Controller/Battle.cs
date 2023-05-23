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
        DebuffMinus();
        for(int i = 0; i < 3; i++)
        {
            if (EnemyData.Instance.Isalive[i])
            {
                EnemyPattern.EnemyPatternStart(EnemyData.Instance.EnemyList[i], EnemyData.Instance.Pat[i]);
                BattleUI battleUI = FindObjectOfType<BattleUI>();
                battleUI.EnemyAttack(i);
            }
        }
    }

    public static void Start_turn()
    {
        BattleData.Instance.CurrentEnergy = BattleData.Instance.MaxEnergy;
        BattleData.Instance.CurrentTurn++;
        BattleData.Instance.Shield = 0;
        for (int i = 0; i < 3; i++)
        {
            if (EnemyData.Instance.Isalive[i])
            {
                EnemyData.Instance.SetPat(i);
            }
            else
            {
                EnemyData.Instance.Pat[i] = 0;
            }
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

    public static void ChangeCurrentShield(float value)
    {
        if(BattleData.Instance.crack > 0 && value > 0)
        {
            value = (float)(int)(value * 0.75f);
        }
        else if(value < 0 && BattleData.Instance.weak > 0)
        {
            value = (float)(int)(value * 0.5f);
        }
        BattleData.Instance.Shield += value;
        if (BattleData.Instance.Shield < 0)
        {
            float temp = BattleData.Instance.Shield;
            BattleData.Instance.Shield = 0;
            ChangeCurrentHealth(temp);
        }
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

    public static void ChangeEnemyShield(int num, float value)
    {
        EnemyData.Instance.Shield[num] += value;
        if (EnemyData.Instance.Shield[num] < 0)
        {
            float temp = EnemyData.Instance.Shield[num];
            EnemyData.Instance.Shield[num] = 0;
            ChangeEnemyHealth(num, temp);
        }
    }

    public static void UseCard(CardStruct card)
    {
        if(card.cost != 99)
        {
            BattleData.Instance.CurrentEnergy -= card.cost;
            BattleData.Instance.UseEnergy = card.cost;
        }
        else
        {
            BattleData.Instance.UseEnergy = BattleData.Instance.CurrentEnergy;
            BattleData.Instance.CurrentEnergy = 0;
            
        }
        BattleData.Instance.Trash.Add(card);
        BattleData.Instance.Hand.Remove(card);
        BattleData.Instance.LastUse = card;
        CardEffect.UseCardEffect(card);
    }

    public static void DebuffMinus()
    {
        if(BattleData.Instance.weak > 0)
        {
            BattleData.Instance.weak--;
        }
        if(BattleData.Instance.crack > 0)
        {
            BattleData.Instance.crack--;
        }
        if(BattleData.Instance.drained > 0)
        {
            BattleData.Instance.drained--;
        }
        if (BattleData.Instance.stun)
        {
            BattleData.Instance.stun = false;
        }
        if(BattleData.Instance.restraint)
        {
            BattleData.Instance.restraint = false;
        }
        if(BattleData.Instance.blind)
        {
            BattleData.Instance.blind = false;
        }
        if(BattleData.Instance.confusion)
        {
            BattleData.Instance.confusion = false;
        }
    }
}
