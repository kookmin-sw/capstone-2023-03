using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DataStructs;
using UnityEngine.EventSystems;
using System;


public enum CardMode
{
    Library, //���̺귯������ ī��UI ���. Ŭ��/������ �� ���� ����
    Select, //ī�� ���� UI���� ī��UI ���. Ŭ��/������ �� ���� ����
    Battle
}

public class CardUI : BaseUI, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    //UI�� ����� ī�� ��ü
    private CardStruct card;

    //ī�� UI�� �̹���, �ؽ�Ʈ ������Ʈ��
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text costText;
    [SerializeField]
    private TMP_Text descriptionText;

    private float scaleOnHover = 1.1f;
    private Vector3 originalScale = Vector3.one;

    private CardMode cardMode;


    public void OnPointerDown(PointerEventData eventData)
    {
        switch (cardMode)
        {
            case CardMode.Library:
                break;
            case CardMode.Select:
                PlayerData.Instance.Deck.Add(card);
                UIManager.Instance.HideUI("CardSelectUI"); //ȹ�� �Ŀ��� �ٷ� �� UI �ݱ�.
                break;
            case CardMode.Battle:
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (cardMode)
        {
            case CardMode.Library:
                break;
            case CardMode.Select:
                transform.localScale = originalScale * scaleOnHover;
                break;
            case CardMode.Battle:
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (cardMode)
        {
            case CardMode.Library:
                break;
            case CardMode.Select:
                transform.localScale = originalScale;
                break;
            case CardMode.Battle:
                break;
        }
    }



    //���ڷ� ���� ī���� �����͸� UI�� �����ִ� �Լ�
    public void ShowCardData(CardStruct showCard, CardMode mode)
    {
        card = showCard;
        cardMode = mode;
        nameText.text = card.name;
        descriptionText.text = card.description;
        costText.text =  card.cost == 99 ? "X" : card.cost.ToString();



        switch(card.rarity)
        {
            case 0:
                nameText.color = Color.white; break;
            case 1:
                nameText.color = Color.magenta; break;
            case 2:
                nameText.color = Color.yellow; break;
        }

        //ī�忡 �Ӽ��� ����� �̹��� ��������
        if (card.attribute != null && card.attribute != "") image.sprite = GameData.Instance.SpriteDic[card.attribute];
        else
        {  
            //���Ӽ��� ���� �ٸ� �ʵ�� �̹��� ���� 
        }

    }

    //�ش� UI�� ǥ���ϴ� ī�� ȹ��... �� ������ UI���� ó���ؾ߰���.
    //��� ī�� UI�� Ŭ�� �� ȹ���� �� �ְ�, Ŭ�� �̺�Ʈ�� �̰����� ó���ϵ�, ��������Ʈ�� �̿��� ������ UI���� Ŭ�� �̺�Ʈ�� �߰����ش�.
}
