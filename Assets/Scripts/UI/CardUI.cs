using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DataStructs;
using UnityEngine.EventSystems;
using System;

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

    public Action PointerDown;
    public Action PointerEnter;
    public Action PointerExit;

    private float scaleOnHover = 1.1f;
    private Vector3 originalScale = Vector3.one;

    private void OnDisable()
    {
        PointerDown = null;
        PointerEnter = null;
        PointerExit = null;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit?.Invoke();
    }



    //인자로 받은 카드의 데이터를 UI로 보여주는 함수
    public void ShowCardData(CardStruct showCard)
    {
        card = showCard;
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

    //카드 크기 확대
    public void HoverEnter()
    {
        transform.localScale = originalScale * scaleOnHover;
    }

    //카드 크기 원래대로
    public void HoverExit()
    {
        transform.localScale = originalScale;
    }    
    
    //덱에 카드 추가
    public void AddCardToDeck()
    {
        PlayerData.Instance.Deck.Add(card);
    }

    //해당 UI가 표시하는 카드 획득... 은 아이템 UI에서 처리해야겠지.
    //대신 카드 UI를 클릭 시 획득할 수 있게, 클릭 이벤트는 이곳에서 처리하되, 델리게이트를 이용해 아이템 UI에서 클릭 이벤트를 추가해준다.
}
