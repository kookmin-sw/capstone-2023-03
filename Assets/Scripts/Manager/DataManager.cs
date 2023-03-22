using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public Dictionary<int, JsonData> DataDictionary = new Dictionary<int, JsonData>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void SaveJson(string dataName)
    {

    }

    public JsonData LoadJson(string dataName)
    {
        string path = $"Assets/Resources/Data/{dataName}.json";
        string JsonString = File.ReadAllText(path);
        JsonData data = JsonMapper.ToObject(JsonString);
        return data;
    }

}
