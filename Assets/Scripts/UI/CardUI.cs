using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DataStructs;
using UnityEngine.EventSystems;
using System;


public enum CardMode
{
    Library, //라이브러리에서 카드UI 모드. 클릭/포인터 시 반응 없음
    Select, //카드 보상 UI에서 카드UI 모드. 클릭/포인터 시 카드 획득
    Battle,
    EventDiscard, //카드 버리기 이벤트에서 카드UI 모드. 클릭/포인터 시 카드 버리기
    ShopDiscard //카드 버리기 상점에서 카드UI 모드. 클릭/포인터 시 카드 버리고, 여러 장 버릴 수 있다.
}

public class CardUI : BaseUI, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    //UI와 연결된 카드 객체
    private CardStruct card;

    //카드 UI의 이미지, 텍스트 오브젝트들
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


    public void OnPointerDown(PointerEventData eventData) //카드 클릭 시 발생
    {
        switch (cardMode)
        {
            case CardMode.Library:
                break;
            case CardMode.Select:
                PlayerData.Instance.Deck.Add(card);
                UIManager.Instance.HideUI("CardSelectUI"); //획득 후에는 바로 카드 선택 UI 닫기.
                break;
            case CardMode.Battle:
                break;
            case CardMode.EventDiscard: //버림 (이벤트)
                PlayerData.Instance.Deck.Remove(card);
                UIManager.Instance.HideUI("LibraryUI"); //버림 후에는 바로 라이브러리 UI 닫기.
                break;
            case CardMode.ShopDiscard: //버림 (상점)
                int newMoney = PlayerData.Instance.Money - ShopData.Instance.DiscardCost;
                if(newMoney > 0) //돈이 남은 경우만
                {
                    PlayerData.Instance.Deck.Remove(card); //버리고 라이브러리 UI 안닫음.
                    PlayerData.Instance.Money = newMoney; //제거 비용만큼 플레이어 돈에서 차감하기
                    PlayerData.Instance.DataChanged(); //덱 변경 알려서 라이브러리를 새로고침하도록!
                    ShopData.Instance.DiscardCost += 25; //삭제 비용 25 추가
                    ShopData.Instance.DataChanged(); //상점 데이터 변경 알려서 삭제비용 새로고침하도록!
                }
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) //카드 마우스 올릴 시
    {
        switch (cardMode)
        {
            case CardMode.Library:
                break;
            case CardMode.Select:
                transform.localScale = originalScale * scaleOnHover; //확대
                break;
            case CardMode.Battle:
                break;
            case CardMode.EventDiscard:
            case CardMode.ShopDiscard:
                transform.localScale = originalScale * scaleOnHover;
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
                transform.localScale = originalScale; //확대 되돌리기
                break;
            case CardMode.Battle:
                break;
            case CardMode.EventDiscard:
            case CardMode.ShopDiscard:
                transform.localScale = originalScale;
                break;
        }
    }



    //인자로 받은 카드의 데이터를 UI로 보여주는 함수
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

        //카드에 속성에 연결된 이미지 가져오기
        if (card.attribute != null && card.attribute != "") image.sprite = GameData.Instance.SpriteDic[card.attribute];
        else
        {  
            //무속성인 경우는 다른 필드로 이미지 결정 
        }

    }
}
