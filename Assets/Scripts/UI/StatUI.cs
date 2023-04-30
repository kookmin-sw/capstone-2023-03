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
        PlayerData.Instance.OnDataChange += UpdateUI; //스탯값에 변화 시 UI 갱신
    }

    private void OnDisable()
    {
        PlayerData.Instance.OnDataChange -= UpdateUI;
    }

    public void UpdateUI()
    {
        StageText.text = $"스테이지 {MapManager.Instance.Stage}";
        EnergyText.text = PlayerData.Instance.Energy.ToString();
        MoneyText.text = PlayerData.Instance.Money.ToString();
        ChannelLevelText.text = PlayerData.Instance.ChannelLevel.ToString();
        ViewerText.text = PlayerData.Instance.Viewers.ToString();
        HpText.text = $"{PlayerData.Instance.CurrentHp}/{PlayerData.Instance.MaxHp}";
    }
}
