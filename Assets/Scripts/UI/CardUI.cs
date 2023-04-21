using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DataStructs;

public class CardUI : BaseUI
{

    //UI와 연결된 카드 객체의 번호
    private int cardIndex;

    //카드 UI의 이미지, 텍스트 오브젝트들
    [SerializeField]
    private Image portrait;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text costText;
    [SerializeField]
    private TMP_Text descriptionText;

    //인자로 받은 카드의 데이터를 UI로 보여주는 함수
    public void ShowCardData(CardStruct card)
    {
        cardIndex = card.index;

        nameText.text = card.name;
        descriptionText.text = card.description;
        costText.text = card.cost.ToString();
    }

    //이거로 고쳐야 할 듯.
    public void ShowCardData(int index)
    {

    }

    //해당 UI가 표시하는 카드 획득... 은 아이템 UI에서 처리해야겠지
}
