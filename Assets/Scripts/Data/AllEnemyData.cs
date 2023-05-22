using DataStructs;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AllEnemyData : Singleton<AllEnemyData>
{
    public List<EnemyStruct> enemyStructs {get; set;} = new List<EnemyStruct>();

    public List<string> NoneEnemyNames { get; set;} = new List<string> {"Knight","Fighter","Peasant","Priest","Thief"};
    public List<string> PirateEnemyNames { get; set; } = new List<string> { "Dealer", "Tanker", "Supporter"};
    public List<string> DruidEnemyNames { get; set; } = new List<string> { "Bird", "Dog", "Bear" };
    public List<string> PriestEnemyNames { get; set; } = new List<string> { "Believer", "Bruth", "Pagan" };
    public List<string> MechanicEnemyNames { get; set; } = new List<string> { "Attacker", "Shielder", "Repairer"};

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        LoadEnemyData();
    }

    public void LoadEnemyData()
    {
        Debug.Log("적 리스트 로드");
        string filePath = "Assets/Resources/Data/EnemyData.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            enemyStructs = JsonMapper.ToObject<List<EnemyStruct>>(jsonData);
        }
    }

    public EnemyStruct GetEnemyData(string name, int stage)
    {
        foreach (EnemyStruct enemyStruct in enemyStructs)
        {
            if (enemyStruct.name == name && enemyStruct.stage == stage)
            {
                return enemyStruct;
            }
        }
        return null;
    }
}
