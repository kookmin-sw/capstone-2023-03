using System.Collections;
using System.Collections.Generic;
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
        HealthData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        CurrentHealth = HealthData.CurrentHp;
        MaximumHealth = HealthData.MaxHp;
        ChangeHP();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHP();
    }

    void DecreaseCurrentHealthBy(float value)
    {
        CurrentHealth -= value;

        if (CurrentHealth <= 0)
        {
            IsAlive = false;
        }
    }

    void ChangeHP()
    {
        transform.GetChild(2).GetComponent<Text>().text = CurrentHealth.ToString() + " / " +MaximumHealth.ToString();
    }
}
