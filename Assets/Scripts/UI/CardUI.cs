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
    public CardStruct Card;

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

    public Action<CardUI> OnCardClicked; //카드 UI 클릭시 실행될 함수들. 다른 UI에서 여기에 등록할 수 있음
    public Action<CardUI> OnCardEntered; //카드 UI로 마우스가 들어올 시 실행될 함수들. 다른 UI에서 여기에 등록할 수 있음
    public Action<CardUI> OnCardExited; //카드 UI에서 마우스가 나갈 시 함수들. 다른 UI에서 여기에 등록할 수 있음


    public void OnPointerDown(PointerEventData eventData) //카드 클릭 시 감지
    {
        OnCardClicked?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData) //카드 마우스 올릴 시 감지
    {
        OnCardEntered?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData) //카드 마우스 탈출 시 감지
    {

        OnCardExited?.Invoke(this); 
    }



    //인자로 받은 카드의 데이터를 UI로 보여주는 함수
    public void ShowCardData(CardStruct showCard)
    {
        Card = showCard;
        nameText.text = Card.name;
        descriptionText.text = Card.description;
        costText.text =  Card.cost == 99 ? "X" : Card.cost.ToString();


        switch(Card.rarity)
        {
            case 0:
                nameText.color = Color.white; break;
            case 1:
                nameText.color = Color.magenta; break;
            case 2:
                nameText.color = Color.yellow; break;
        }

        //카드에 속성에 연결된 이미지 가져오기
        if (Card.attribute != null && Card.attribute != "") image.sprite = GameData.Instance.SpriteDic[Card.attribute];
        else
        {  
            //무속성인 경우는 다른 필드로 이미지 결정 
        }

    }

    public void CardBig() //해당 카드 UI 확대
    {
        transform.localScale = Vector3.one * 1.1f; //확대
    }

    public void CardSmall() //해당 카드 UI 축소
    {
        transform.localScale = Vector3.one; //축소
    }

}
