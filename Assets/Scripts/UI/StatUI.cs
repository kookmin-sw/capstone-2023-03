using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField]
    public TMP_Text StageText;
    [SerializeField]
    public TMP_Text HpText;
    [SerializeField]
    public TMP_Text EnergyText; 
    [SerializeField]
    public TMP_Text MoneyText;
    [SerializeField]
    public TMP_Text ChannelLevelText;
    [SerializeField]
    public TMP_Text ViewerText;


    private void OnEnable()
    {
        UpdateUI();
        PlayerData.Instance.OnDataChange += UpdateUI; //���Ȱ��� ��ȭ �� UI ����
        LevelManager.Instance.OnLevelEnter += UpdateUI; //�������� ��ȭ �� UI ����
    }

    private void OnDisable()
    {
        PlayerData.Instance.OnDataChange -= UpdateUI;
        LevelManager.Instance.OnLevelEnter -= UpdateUI; 

    }

    public void UpdateUI()
    {
        StageText.text = $"�������� {LevelManager.Instance.Level}";
        EnergyText.text = PlayerData.Instance.Energy.ToString();
        MoneyText.text = PlayerData.Instance.Money.ToString();
        ChannelLevelText.text = PlayerData.Instance.ChannelLevel.ToString();
        ViewerText.text = PlayerData.Instance.Viewers.ToString();
        HpText.text = $"{PlayerData.Instance.CurrentHp}/{PlayerData.Instance.MaxHp}";
    }
}
