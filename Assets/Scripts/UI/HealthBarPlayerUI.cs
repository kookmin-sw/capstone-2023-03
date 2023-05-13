using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayerUI : MonoBehaviour
{

    public PlayerData HealthData;
    public bool IsAlive = true;
    public float CurrentHealth = 100;
    public float MaximumHealth = 100;

    public bool HasAnimationWhenHealthChanges = true;
    public float AnimationDuration = 0.1f;

    TextMeshProUGUI TextUI;
    Image image;

    public float CurrentHealthPercentage
    {
        get
        {
            return (CurrentHealth / MaximumHealth) * 100;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TextUI = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();
        if (GameObject.Find("PlayerData") != null)
        {
            HealthData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
            CurrentHealth = HealthData.CurrentHp;
            MaximumHealth = HealthData.MaxHp;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            ChangeCurrentHealth(-5);
        }
        else if(Input.GetKeyUp(KeyCode.Alpha6))
        {
            ChangeCurrentHealth(5);
        }
        ChangeHPText();
    }

    void ChangeCurrentHealth(float value)
    {
        CurrentHealth += value;

        if (CurrentHealth <= 0)
        {
            IsAlive = false;
        }
        if(CurrentHealth > MaximumHealth)
        {
            CurrentHealth = MaximumHealth;
        }
    }

    void ChangeHPText() // Text의 텍스트 내용을 CurrentHealth / MaximumHealth로 바꿔주는 함수
    {
        if (IsAlive)
        {
            TextUI.text = CurrentHealth + " / " + MaximumHealth;
        }
        else
        {
            TextUI.text = "Dead";
        }

        image.fillAmount = CurrentHealth / MaximumHealth;
    }

}
