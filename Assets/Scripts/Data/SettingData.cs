using DataStructs;
using LitJson;
using System.IO;
using UnityEngine;

public class SettingData : Singleton<SettingData>
{
    public SettingStruct Setting { get; set; } = new SettingStruct();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        LoadSettingData();
    }

    private void OnApplicationQuit()
    {
        SaveSettingData(); //끌 때 설정값 저장
    }

    private void LoadSettingData() //저장된 설정 데이터들 가져오기
    {
        Debug.Log("Setting Data Load");
        string filePath = "Assets/Resources/Data/Setting.json";
        if (File.Exists(filePath)) //두번째 실행 이후부터는 저장된 세팅 데이터를 가져온다.
        {
            string jsonData = File.ReadAllText(filePath);
            Setting = JsonMapper.ToObject<SettingStruct>(jsonData);
        }
        else //처음 게임을 실행할 때는 기본 설정으로.
        {
            Setting.tutorial = true;
            Setting.bgm = 50;
            Setting.effect = 50;  
        }
    }

    private void SaveSettingData() //게임 종료 시, 설정 데이터 저장.
    {
        string filePath = "Assets/Resources/Data/Setting.json";
        string jsonData = JsonMapper.ToJson(Setting);
        File.WriteAllText(filePath, jsonData);
    }
}
