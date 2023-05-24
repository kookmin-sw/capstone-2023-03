using DataStructs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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
        string special = enemy.special;

        Battle.ChangeCurrentShield(-6);
    }
}
