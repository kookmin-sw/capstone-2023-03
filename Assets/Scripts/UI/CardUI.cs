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

    //UI와 연결된 카드 객체의 번호
    public int cardIndex;

    //인자로 받은 카드의 데이터를 UI로 보여주는 함수
    public void ShowCardData(CardStruct card)
    {
        cardIndex = card.index;

        nameText.text = card.name;
        descriptionText.text = card.description;
        costText.text = card.cost.ToString();
    }

    //해당 UI가 표시하는 카드 획득... 은 아이템 UI에서 처리해야겠지
    public void GetCard()
    {

    }
}
