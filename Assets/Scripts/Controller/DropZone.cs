using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DataStructs;

public class DropZone : MonoBehaviour, IDropHandler
{
    GameObject CardUI;
    CardStruct Card;
    NoticeUI noticeUI;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        CardUI = eventData.pointerDrag;
        Card = CardUI.GetComponent<CardUI>().Card;
        if(Card != null && Card.cost <= BattleData.Instance.CurrentEnergy)
        {
            Battle.UseCard(Card);

            Destroy(CardUI);
        }
        else
        {
            noticeUI = FindObjectOfType<NoticeUI>();
            noticeUI.ShowNotice("에너지가 부족합니다 !");
        }
    }

}
