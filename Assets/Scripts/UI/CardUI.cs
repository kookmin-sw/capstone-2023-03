using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    public Card BindedCard { get; set; }

    private void Awake()
    {
        //UI 요소들 가져오기
    }

    public void ShowCard(Card card)
    {
        BindedCard = card;
        
        //카드의 이름 이미지 등을 UI 요소로 보여주기
    }


}
