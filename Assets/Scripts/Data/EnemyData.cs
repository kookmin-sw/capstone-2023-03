using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DataStructs;
using Unity.VisualScripting;

public class EnemyData : Singleton<EnemyData>
{
    public List<bool> Isalive { get; set; } = new List<bool> { false,false,false };
    public List<float> HP { get; set; } = new List<float> { 30, 20, 10 };
    public List<float> Shield { get; set; } = new List<float> { 0, 0, 0 };
    public EnemyStruct Enemy1;
    public EnemyStruct Enemy2;
    public EnemyStruct Enemy3;
    public List<EnemtStruct> EnemyList = new List<EnemtStruct> { Enemy1, Enemy2, Enemy3 };


    // Start is called before the first frame update
    protected override void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(int num, string name, int stage)
    {
        Isalive[num - 1] = true;
        int random;
        switch (num)
        {
            case 1:
                Enemy1 = AllEnemyData.Instance.GetEnemyData(name, stage);
                random = UnityEngine.Random.Range(Enemy1.minHP, Enemy1.maxHP+1);
                HP[0] = random;
                break;
            case 2:
                Enemy2 = AllEnemyData.Instance.GetEnemyData(name, stage);
                random = UnityEngine.Random.Range(Enemy2.minHP, Enemy2.maxHP + 1);
                HP[1] = random;
                break;
            case 3:
                Enemy3 = AllEnemyData.Instance.GetEnemyData(name, stage);
                random = UnityEngine.Random.Range(Enemy3.minHP, Enemy3.maxHP + 1);
                HP[2] = random;
                break;
        }
    }

    public void Reset()
    {
        Isalive = new List<bool> { false, false, false };
        HP = new List<float> { 0, 0, 0 };
        Shield = new List<float> { 0, 0, 0 };
        Enemy1 = new EnemyStruct();
        Enemy2 = new EnemyStruct();
        Enemy3 = new EnemyStruct();
    }

    public void SetPat(int num)
    {
        int random;
        EnemtStruct enemy = EnemyList[num];
        random = UnityEngine.Random.Range(0,4);
    }
}
