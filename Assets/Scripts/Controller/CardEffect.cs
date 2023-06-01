using DataStructs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class CardEffect : MonoBehaviour
{
    private void OnEnable()
    {

    }

    private void OnDisable()
    {


    }

    public static async void UseCardEffect(CardStruct card)
    {
        BattleUI battleUI = FindObjectOfType<BattleUI>();

        int index = card.index;
        string name = card.name;
        string type = card.type;
        string attack_type = card.attack_type;
        string target = card.target;
        int damage = card.damage;
        int times = card.times;
        string special = card.special;
        int special_stat = card.special_stat;
        if (type == "Attack")
        {
            if (target == "One")
            {
                TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
                UIManager.Instance.ShowUI("SelectEnemyUI", false).GetComponent<SelectEnemyUI>().init(taskCompletionSource);


                await taskCompletionSource.Task;
                if (times > 0)
                {
                    for (int i = 0; i < times; i++)
                    {

                        if (attack_type == "Physical")
                        {
                            Battle.ChangeEnemyShield(BattleData.Instance.SelectedEnemy, -(damage + BattleData.Instance.Str));
                        }
                        else if (attack_type == "Magic")
                        {
                            Battle.ChangeEnemyShield(BattleData.Instance.SelectedEnemy, -(damage + BattleData.Instance.Int));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < BattleData.Instance.UseEnergy; i++)
                    {
                        if (attack_type == "Physical")
                        {
                            Battle.ChangeEnemyShield(BattleData.Instance.SelectedEnemy, -(damage + BattleData.Instance.Str));
                        }
                        else if (attack_type == "Magic")
                        {
                            Battle.ChangeEnemyShield(BattleData.Instance.SelectedEnemy, -(damage + BattleData.Instance.Int));
                        }
                    }
                }
                if(special != "None")
                {
                    Battle.EnemyDebuff(BattleData.Instance.SelectedEnemy, special, special_stat);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    if (EnemyData.Instance.Isalive[i])
                    {
                        if (times > 0)
                        {
                            for (int j = 0; j < times; j++)
                            {

                                if (attack_type == "Physical")
                                {
                                    Battle.ChangeEnemyShield(i, -(damage + BattleData.Instance.Str));
                                }
                                else if (attack_type == "Magic")
                                {
                                    Battle.ChangeEnemyShield(i, -(damage + BattleData.Instance.Int));
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < BattleData.Instance.UseEnergy; j++)
                            {
                                if (attack_type == "Physical")
                                {
                                    Battle.ChangeEnemyShield(i, -(damage + BattleData.Instance.Str));
                                }
                                else if (attack_type == "Magic")
                                {
                                    Battle.ChangeEnemyShield(i, -(damage + BattleData.Instance.Int));
                                }
                            }
                        }
                        if(special != "None")
                        {
                            Battle.EnemyDebuff(i, special, special_stat);
                        }
                    }
                }
            }
        }
        else if (type == "Skill")
        {
            switch (index)
            {
                case 1:
                    Battle.ChangeCurrentShield(5, -1);
                    break;
                case 22:
                    BattleData.Instance.Int += 2;
                    BattleData.Instance.burn = true;
                    break;
            }
        }
        else if (type == "Viewer")
        {

        }
        else
        {
            if(target == "Ran")
            {
                int num = Battle.RandomEnemy();
                if (times > 0)
                {
                    for (int i = 0; i < times; i++)
                    {
                        Battle.ChangeEnemyShield(num, -damage);
                    }
                }
            }
            else if(target == "All")
            {
                for (int i = 0; i < 3; i++)
                {
                    if (EnemyData.Instance.Isalive[i])
                    {
                        if (times > 0)
                        {
                            for (int j = 0; j < times; j++)
                            {

                                Battle.ChangeEnemyShield(i, -damage);
                            }
                        }
                    }
                }
            }
            else
            {

            }
            battleUI.Draw();
        }
        
    }
}
