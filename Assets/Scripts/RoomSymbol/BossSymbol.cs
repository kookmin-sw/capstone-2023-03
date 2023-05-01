using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSymbol : RoomSymbol
{
    public override void TalkStart()
    {
        UIManager.Instance.ShowUI("DialogUI")
            .GetComponent<DialogUI>()
            .Init(index, AfterFight); //대화가 끝나면 전투 UI 오픈

    }

    public void Fight()
    {
        //전투 UI 열기
        //전투 UI 종료 시 AfterFight 호출
    }

    //전투 후 대화
    public void AfterFight()
    {
        UIManager.Instance.ShowUI("DialogUI")
            .GetComponent<DialogUI>()
            .Init(index + Define.BOSS_AFTER_INDEX, SelectOpen); //대화가 끝나면 영입 제의 팝업 오픈 
    }

    //영입 제의 팝업 오픈
    public void SelectOpen()
    {
        UIManager.Instance.ShowUI("SelectUI")
            .GetComponent<SelectUI>()
            .Init(
                "보스에게 크루원 영입 제의를 하시겠습니까?",
                AfterNego, //예 선택 시 영입 보상 호출
                FightEnd //아니오 선택 시 전투 보상 호출
            );
    }

    public void FightEnd() //전투 끝나고 영입 제의를 하지 않을 시 호출
    {

        //스탯을 ... 만큼 증가
        PlayerData.Instance.CurrentHp -= 10; //임시
        PlayerData.Instance.Money += GameData.Instance.RewardDic[LevelManager.Instance.Level + Define.BOSS_INDEX].money; 
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[LevelManager.Instance.Level + Define.BOSS_INDEX].viewers;



        //보상 카드 UI 닫을 시, TalkEnd 호출
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.Init(TalkEnd);
        cardSelectUI.BossBattleReward();
    }

    //영입 제의 했을 때 후속 대화
    public void AfterNego()
    {
        UIManager.Instance.ShowUI("DialogUI")
            .GetComponent<DialogUI>()
            .Init(index + Define.BOSS_NEGO_INDEX, NegotiateEnd); //대화가 끝나면 협상 보상 획득
    }


    public void NegotiateEnd() //영입 후 호출
    {
        //스탯을 ... 만큼 증가
        PlayerData.Instance.Money += GameData.Instance.RewardDic[LevelManager.Instance.Level + Define.BOSS_INDEX].money / 2;
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[LevelManager.Instance.Level + Define.BOSS_INDEX].viewers / 2;

        //보상 카드 UI 닫을 시, TalkEnd 호출
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.Init(TalkEnd);
        cardSelectUI.BossNegoReward(index);
    }

    public override void TalkEnd()
    {
        base.TalkEnd();
        LevelManager.Instance.LevelClear();
    }
}
