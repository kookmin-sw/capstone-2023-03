
using UnityEngine;

public class EnemySymbol : RoomSymbol
{

    public override void TalkStart()
    {
        UIManager.Instance.ShowUI("DialogUI")
        .GetComponent<DialogUI>()
        .Init(index, SelectOpen); //대화가 끝나면 협상 팝업 오픈

    }

    //협상 팝업 오픈
    public void SelectOpen()
    {
        UIManager.Instance.ShowUI("SelectUI")
            .GetComponent<SelectUI>()
            .Init(
                "협상하시겠습니까?", 
                TryNegotiate, //예 선택 시 협상 시도 함수 호출
                () => { UIManager.Instance.ShowUI("DialogUI").GetComponent<DialogUI>().Init(index + Define.FIGHT_INDEX, FightEnd); } //아니오 선택 시 전투 대화 후 전투 UI 호출
            );
    }

    //협상 시도
    public void TryNegotiate()
    {
        float random = Random.Range(0f, 1f);

        //협상 고르면 랜덤한 확률로 협상 대화, 실패하면 협상 실패 대화 호출 후 전투 호출
        if(random < 0.3f && LevelManager.Instance.NegoInLevel == false) //일단 30퍼 확률 + 이 스테이지에서 협상을 한 적 없으면 협상 성공
        {
            UIManager.Instance.ShowUI("DialogUI").GetComponent<DialogUI>().Init(index + Define.NEGO_INDEX, NegotiateEnd);
        }
        else
        {
            UIManager.Instance.ShowUI("DialogUI").GetComponent<DialogUI>().Init(index + Define.NEGOFAIL_INDEX, FightEnd);
        }
    }

    public void Fight()
    {
        //전투 UI 열기
        //전투 UI 닫을 시, FightEnd 호출
    }

    public void FightEnd() //전투 끝날 시 호출
    {

        //스탯을 현재 레벨에 맞는 보상 만큼 증가
        PlayerData.Instance.CurrentHp -= 10; //임시
        PlayerData.Instance.Money += GameData.Instance.RewardDic[LevelManager.Instance.Level].money; //현재 레벨에 해당하는 보상을 가져와서, 스탯에 추가
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[LevelManager.Instance.Level].viewers;

        //보상 카드 UI 닫을 시, TalkEnd 호출
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.Init(TalkEnd);
        cardSelectUI.BattleReward();
    }

    public void NegotiateEnd() //협상 성공 후 호출
    {
        //스탯을 현재 레벨에 맞는 보상/2 만큼 증가
        PlayerData.Instance.Money += GameData.Instance.RewardDic[LevelManager.Instance.Level].money/2;
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[LevelManager.Instance.Level].viewers/2;

        //보상 카드 UI 닫을 시, TalkEnd 호출
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.Init(TalkEnd);
        cardSelectUI.NegoReward(index); //현재 인덱스에 맞는 잡몹 동료 카드 획득
    }

    public override void TalkEnd()
    {
        base.TalkEnd();

    }
}
