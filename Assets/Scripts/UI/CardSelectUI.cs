using DataStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardSelectUI : MonoBehaviour
{
    [SerializeField]
    GameObject rewardView; //보상 창 카드 띄우는 곳

    [SerializeField]
    TMP_Text rewardText; //보상 창 안내문

    [SerializeField]
    Button discardButton;

    List<CardStruct> rewardCards = new List<CardStruct>(); //보상으로 선택 가능한 카드 데이터들

    private Action CloseAction; //창 닫을 때 발생하는 이벤트


    private void OnDisable()
    {
        CloseAction?.Invoke();
    }

    public void Init(Action CloseCallback)
    {
        CloseAction = CloseCallback;
    }

    private void ShowReward()
    {
        //보상 카드들을 UI에 표시
        for (int i = 0; i < rewardCards.Count; i++)
        {
            CardUI cardUI = AssetLoader.Instance.Instantiate("Prefabs/UI/CardUI", rewardView.transform).GetComponent<CardUI>();
            cardUI.ShowCardData(rewardCards[i], CardMode.Select); //현재 보상 카드를 보여줌.
        }
    }

    //전투 보상으로 띄우는 경우 호출
    public void BattleReward()
    {

        rewardText.text = "전투에서 승리하셨습니다!\r\n보상으로 카드를 한 장 가져가세요.";

        //전투의 보상으로 얻을 공격, 스킬 카드들을 쿼리. 그리고 레어도 별로 또 나눈다.
        List<CardStruct> rewardCardsPool = GameData.Instance.CardList.Where(card => card.type == "Attack" || card.type == "Skill").ToList();
        List<CardStruct> rarity0Cards = rewardCardsPool.Where(card => card.rarity == 0).ToList();
        List<CardStruct> rarity1Cards = rewardCardsPool.Where(card => card.rarity == 1).ToList();
        List<CardStruct> rarity2Cards = rewardCardsPool.Where(card => card.rarity == 2).ToList();

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
        ShowReward();
    }

    //보스와의 전투 보상
    public void BossBattleReward()
    {
        rewardText.text = "보스와의 전투에서 승리하셨습니다!\r\n보상으로 희귀한 카드를 한 장 가져가세요.";

        //전투의 보상으로 얻을 카드들을 쿼리. 보스 보상으로는 레어 이상의 카드만 나옴
        List<CardStruct> rewardCardsPool = GameData.Instance.CardList.Where(card => card.type == "Attack" || card.type == "Skill").ToList();
        List<CardStruct> rarity1Cards = rewardCardsPool.Where(card => card.rarity == 1).ToList();
        List<CardStruct> rarity2Cards = rewardCardsPool.Where(card => card.rarity == 2).ToList();

        for (int i = 0; i < 3; i++)
        {
            float random = Random.Range(0f, 1f);

            //확률에 따라 레어, 유니크 카드풀 중에서 한 카드를 골라 보상 카드로 지정
            if (random < 0.63f)
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

        ShowReward();
    }

    //협상 후 카드 보상
    public void NegoReward(int index)
    {
        rewardText.text = "협상에 성공했습니다!\r\n상대 시청자가 합류합니다.";

        discardButton.gameObject.SetActive(false); //협상 시에는 반드시 카드 택하기


        List<CardStruct> rewardCardsPool;

        //타입이 잡몹인 카드 중, 에너미 심볼의 인덱스에 따른 카드를 가져온다.
        switch (index)
        {
            //일반 잡몹이면 카드의 타입이 잡몹 카드들인 카드들을 가져오되, '스테이지에 맞는' 잡몹 카드를 가져옴. 3스테이지면 mob3 타입의 카드들만 가져옴.
            //예시로, 3스테이지면 mob3 타입의 카드들만 가져오게 된다.
            case 0: 
                rewardCardsPool = GameData.Instance.CardList
                    .Where(card => card.type == $"Mob{StageManager.Instance.Stage}")
                    .ToList();
                break;
            //그 외인 경우, index는 현재 Theme의 번호와 같다. Theme에 해당하는 Enum의 텍스트(예를 들어 index가 1이면 Define.ThemeType.Pirate와 대응)
            //를 가져오고, 현재 스테이지의 번호를 더하면, 해적 테마의 1스테이지인 경우 Pirate1 이라는 문자열이 만들어 진다.
            //card의 type이 Pirate1인 카드들을 가져와서 풀에 추가한다.
            default:
                rewardCardsPool = GameData.Instance.CardList
                    .Where(card => card.type == $"{StageManager.Instance.Theme}{StageManager.Instance.Stage}")
                    .ToList();
                break;
        }

        rewardCards.Add(rewardCardsPool[Random.Range(0, rewardCardsPool.Count)]);

        ShowReward();
    }

    //보스 영입 시 카드 보상
    public void BossNegoReward(int enemyIndex)
    {
        rewardText.text = "보스를 크루원으로 영입하였습니다!\r\n강력한 동료가 되어 줄 것입니다.";

        discardButton.gameObject.SetActive(false); //협상 시에는 반드시 카드 택하기

        //타입이 보스인 카드 중, 에너미의 인덱스번째의 카드를 가져온다.
        rewardCards.Add(GameData.Instance.CardList.Where(card => card.type == $"{StageManager.Instance.Theme}Boss").ElementAtOrDefault(0));
        ShowReward();
    }

    //레벨 업 후 카드 보상
    public void LevelUpReward()
    {

        rewardText.text = "채널 레벨이 상승하였습니다!\r\n애청자들이 강력한 지원을 보내왔습니다.";

        discardButton.gameObject.SetActive(false); //반드시 카드 택하기

        //type == "애청자" 인 경우
        List<CardStruct> viewerCards = GameData.Instance.CardList.Where(card => card.type == "Viewer").ToList();

        int index = Random.Range(0, viewerCards.Count);
        rewardCards.Add(viewerCards[index]);

        ShowReward();
    }

    //버리기 버튼 클릭
    public void ExitButtonClick()
    {
        UIManager.Instance.HideUI("CardSelectUI");
    }
}
