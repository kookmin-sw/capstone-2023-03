using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : BaseUI
{

    //카드 UI의 이미지, 텍스트 오브젝트들
    [SerializeField]
    private Image portrait;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text costText;
    [SerializeField]
    private TMP_Text descriptionText;

    //UI와 연결된 카드 객체
    public CardData BindedCard { get; set; }

    //인자로 받은 카드의 데이터를 UI로 보여주는 함수
    public void ShowCardData(CardData card)
    {
        BindedCard = card;

        nameText.text = card.name;
        descriptionText.text = card.description;
        costText.text = card.cost.ToString();
    }


}
