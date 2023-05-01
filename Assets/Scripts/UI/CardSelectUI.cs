using DataStructs;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.UI;

public class CardSelectUI : MonoBehaviour
{
    [SerializeField]
    GameObject rewardView; //���� â ī�� ���� ��

    [SerializeField]
    TMP_Text rewardText; //���� â �ȳ���

    [SerializeField]
    Button discardButton;

    List<CardStruct> rewardCards = new List<CardStruct>(); //�������� ���� ������ ī�� �����͵�

    private Action CloseAction; //â ���� �� �߻��ϴ� �̺�Ʈ


    private void OnDisable()
    {
        CloseAction?.Invoke();
    }

    public void Init(Action CloseCallback)
    {
        CloseAction = CloseCallback;
    }

    private void ShowReward()
    {
        //���� ī����� UI�� ǥ��
        for (int i = 0; i < rewardCards.Count; i++)
        {
            CardUI cardUI = AssetLoader.Instance.Instantiate("Prefabs/UI/CardUI", rewardView.transform).GetComponent<CardUI>();
            cardUI.ShowCardData(rewardCards[i], CardMode.Select); //���� ���� ī�带 ������.
        }
    }

    //���� �������� ���� ��� ȣ��
    public void BattleReward()
    {

        rewardText.text = "�������� �¸��ϼ̽��ϴ�!\r\n�������� ī�带 �� �� ����������.";

        //������ �������� ���� ī����� ����. �׸��� ��� ���� �� ������.
        List<CardStruct> rewardCardsPool = GameData.Instance.CardList.Where(card => card.type == "����" || card.type == "��ų").ToList();
        List<CardStruct> rarity0Cards = rewardCardsPool.Where(card => card.rarity == 0).ToList();
        List<CardStruct> rarity1Cards = rewardCardsPool.Where(card => card.rarity == 1).ToList();
        List<CardStruct> rarity2Cards = rewardCardsPool.Where(card => card.rarity == 2).ToList();

        for (int i = 0; i < 3; i++)
        {
            float random = Random.Range(0f, 1f);

            //Ȯ���� ���� �븻, ����, ����ũ ī��Ǯ �߿��� �� ī�带 ��� ���� ī��� ����
            if (random < 0.63f)
            {
                int index = Random.Range(0, rarity0Cards.Count);
                rewardCards.Add(rarity0Cards[index]);
            }
            else if (random < 0.95f)
            {
                int index = Random.Range(0, rarity1Cards.Count);
                rewardCards.Add(rarity1Cards[index]);
            }
            else
            {
                int index = Random.Range(0, rarity2Cards.Count);
                rewardCards.Add(rarity2Cards[index]);
            }
        }

        //���� ī����� UI�� ǥ��
        ShowReward();
    }

    //�������� ���� ����
    public void BossBattleReward()
    {
        rewardText.text = "�������� �������� �¸��ϼ̽��ϴ�!\r\n�������� ����� ī�带 �� �� ����������.";

        //������ �������� ���� ī����� ����. ���� �������δ� ���� �̻��� ī�常 ����
        List<CardStruct> rewardCardsPool = GameData.Instance.CardList.Where(card => card.type == "����" || card.type == "��ų").ToList();
        List<CardStruct> rarity1Cards = rewardCardsPool.Where(card => card.rarity == 1).ToList();
        List<CardStruct> rarity2Cards = rewardCardsPool.Where(card => card.rarity == 2).ToList();

        for (int i = 0; i < 3; i++)
        {
            float random = Random.Range(0f, 1f);

            //Ȯ���� ���� ����, ����ũ ī��Ǯ �߿��� �� ī�带 ��� ���� ī��� ����
            if (random < 0.63f)
            {
                int index = Random.Range(0, rarity1Cards.Count);
                rewardCards.Add(rarity1Cards[index]);
            }
            else
            {
                int index = Random.Range(0, rarity2Cards.Count);
                rewardCards.Add(rarity2Cards[index]);
            }
        }

        ShowReward();
    }

    //���� �� ī�� ����
    public void NegoReward(int enemyIndex)
    {
        rewardText.text = "���� �����߽��ϴ�!\r\n��� ��û�ڰ� �շ��մϴ�.";

        LevelManager.Instance.NegoInLevel = true; //���� �������������� ���� �ٽ� �ص� �ȵȴ�

        discardButton.gameObject.SetActive(false); //���� �ÿ��� �ݵ�� ī�� ���ϱ�

        //Ÿ���� ����� ī�� ��, ���ʹ��� �ε�����°�� ī�带 �����´�.
        rewardCards.Add(GameData.Instance.CardList.Where(card => card.type == "���").ElementAtOrDefault(enemyIndex));
        ShowReward();
    }

    //���� ���� �� ī�� ����
    public void BossNegoReward(int enemyIndex)
    {
        rewardText.text = "������ ũ������� �����Ͽ����ϴ�!\r\n������ ���ᰡ �Ǿ� �� ���Դϴ�.";

        discardButton.gameObject.SetActive(false); //���� �ÿ��� �ݵ�� ī�� ���ϱ�

        //Ÿ���� ������ ī�� ��, ���ʹ��� �ε�����°�� ī�带 �����´�.
        rewardCards.Add(GameData.Instance.CardList.Where(card => card.type == "����").ElementAtOrDefault(enemyIndex - Define.BOSS_INDEX));
        ShowReward();
    }

    //���� �� �� ī�� ����
    public void LevelUpReward()
    {
        //type == "��û��" �� ���
    }

    //������ ��ư Ŭ��
    public void ExitButtonClick()
    {
        UIManager.Instance.HideUI("CardSelectUI");
    }
}
