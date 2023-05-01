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
        SaveSettingData(); //�� �� ������ ����
    }

    private void LoadSettingData() //����� ���� �����͵� ��������
    {
        Debug.Log("Setting Data Load");
        string filePath = "Assets/Resources/Data/Setting.json";
        if (File.Exists(filePath)) //�ι�° ���� ���ĺ��ʹ� ����� ���� �����͸� �����´�.
        {
            string jsonData = File.ReadAllText(filePath);
            Setting = JsonMapper.ToObject<SettingStruct>(jsonData);
        }
        else //ó�� ������ ������ ���� �⺻ ��������.
        {
            Setting.tutorial = true;
            Setting.bgm = 50;
            Setting.effect = 50;  
        }
    }

    private void SaveSettingData() //���� ���� ��, ���� ������ ����.
    {
        string filePath = "Assets/Resources/Data/Setting.json";
        string jsonData = JsonMapper.ToJson(Setting);
        File.WriteAllText(filePath, jsonData);
    }
}
