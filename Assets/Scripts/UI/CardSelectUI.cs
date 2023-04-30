using DataStructs;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class CardSelectUI : MonoBehaviour
{
    [SerializeField]
    GameObject rewardView;

    List<CardStruct> rewardCards;

    private Action CloseAction;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void OnDisable()
    {
        CloseAction?.Invoke();
    }

    public void Init(Action CloseCallback)
    {
        CloseAction = CloseCallback;
    }

    public void BattleReward()
    {
        //전투의 보상으로 얻을 카드들을 쿼리. 그리고 레어도 별로 또 나눈다.
        List<CardStruct> rewardCardsPool = GameData.Instance.CardList.Where(card => card.type == "공격" || card.type == "스킬").ToList();
        List<CardStruct> rarity0Cards = rewardCardsPool.Where(card => card.rarity == 0).ToList();
        List<CardStruct> rarity1Cards = rewardCardsPool.Where(card => card.rarity == 1).ToList();
        List<CardStruct> rarity2Cards = rewardCardsPool.Where(card => card.rarity == 2).ToList();

        rewardCards = new List<CardStruct>();

        for (int i = 0; i < 3; i++)
        {
            float random = Random.Range(0f, 1f);

            //확률에 따라 노말, 레어, 유니크 카드풀 중에서 한 카드를 골라 보상 카드로 지정
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

        //보상 카드들을 UI에 표시
        for (int i = 0; i < rewardCards.Count; i++)
        {
            CardUI cardUI = AssetLoader.Instance.Instantiate("Prefabs/UI/CardUI", rewardView.transform).GetComponent<CardUI>();
            cardUI.ShowCardData(rewardCards[i]); //현재 보상 카드를 보여줌.
            cardUI.PointerDown += () =>
            {
                cardUI.AddCardToDeck(); //해당 카드 UI의 카드 획득 기능을 해당 카드의 클릭 이벤트가 참조하도록 한다.
                UIManager.Instance.HideUI("CardSelectUI"); //획득 후에는 바로 이 UI 닫기.
            };
            cardUI.PointerEnter += cardUI.HoverEnter; //해당 카드 UI의 확대/축소 기능을 해당 카드의 호버 이벤트가 참조하도록 한다.
            cardUI.PointerExit += cardUI.HoverExit;
        }
    }

    //협상 후 카드 보상
    public void NegoReward()
    {

    }

    //레벨 업 후 카드 보상
    public void LevelUpReward()
    {

    }
}
