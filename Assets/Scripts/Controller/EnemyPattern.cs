using DataStructs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Net.Http.Headers;

public class EnemyPattern : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {

    }

    private void OnDisable()
    {


    }

    public static void EnemyPatternStart(EnemyStruct enemy, int pat)
    {
        int indext = enemy.index;
        string name = enemy.name;
        string special = enemy.special;
        int stage = StageManager.Instance.Stage;
        switch (name)
        {
            case "Fighter":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Knight":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Peasant":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Priest":
                switch (pat)
                {
                    case 1:
                        
                        if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        
                        if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        
                        if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Thief":
                switch (pat)
                {
                    case 1:
                        Battle.ChangeCurrentShield(-10);
                        break;
                    case 2:
                        Battle.ChangeCurrentShield(-10);
                        break;
                    case 3:
                        Battle.ChangeCurrentShield(-10);
                        break;
                    case 4:
                        Battle.ChangeCurrentShield(-10);
                        break;
                }
                break;
            case "Dealer":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Tanker":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Supporter":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Dog":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Bear":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Bird":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Believer":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Bruth":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Pagan":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Attacker":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Shielder":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "Repairer":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "PirateBoss":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "DruidBoss":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "PriestBoss":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "MechanicBoss":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
            case "LastBoss":
                switch (pat)
                {
                    case 1:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 2:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 3:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                    case 4:
                        if (stage == 1)
                        {
                            Battle.ChangeCurrentShield(-6);
                        }
                        else if (stage == 2)
                        {
                            Battle.ChangeCurrentShield(-8);
                        }
                        else if (stage == 3)
                        {
                            Battle.ChangeCurrentShield(-10);
                        }
                        break;
                }
                break;
        }
    }
}
