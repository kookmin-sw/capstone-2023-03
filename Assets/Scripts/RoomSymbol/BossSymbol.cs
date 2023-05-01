using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSymbol : RoomSymbol
{
    public override void TalkStart()
    {
        UIManager.Instance.ShowUI("DialogUI")
            .GetComponent<DialogUI>()
            .Init(index, AfterFight); //��ȭ�� ������ ���� UI ����

    }

    public void Fight()
    {
        //���� UI ����
        //���� UI ���� �� AfterFight ȣ��
    }

    //���� �� ��ȭ
    public void AfterFight()
    {
        UIManager.Instance.ShowUI("DialogUI")
            .GetComponent<DialogUI>()
            .Init(index + Define.BOSS_AFTER_INDEX, SelectOpen); //��ȭ�� ������ ���� ���� �˾� ���� 
    }

    //���� ���� �˾� ����
    public void SelectOpen()
    {
        UIManager.Instance.ShowUI("SelectUI")
            .GetComponent<SelectUI>()
            .Init(
                "�������� ũ��� ���� ���Ǹ� �Ͻðڽ��ϱ�?",
                AfterNego, //�� ���� �� ���� ���� ȣ��
                FightEnd //�ƴϿ� ���� �� ���� ���� ȣ��
            );
    }

    public void FightEnd() //���� ������ ���� ���Ǹ� ���� ���� �� ȣ��
    {

        //������ ... ��ŭ ����
        PlayerData.Instance.CurrentHp -= 10; //�ӽ�
        PlayerData.Instance.Money += GameData.Instance.RewardDic[LevelManager.Instance.Level + Define.BOSS_INDEX].money; 
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[LevelManager.Instance.Level + Define.BOSS_INDEX].viewers;



        //���� ī�� UI ���� ��, TalkEnd ȣ��
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.Init(TalkEnd);
        cardSelectUI.BossBattleReward();
    }

    //���� ���� ���� �� �ļ� ��ȭ
    public void AfterNego()
    {
        UIManager.Instance.ShowUI("DialogUI")
            .GetComponent<DialogUI>()
            .Init(index + Define.BOSS_NEGO_INDEX, NegotiateEnd); //��ȭ�� ������ ���� ���� ȹ��
    }


    public void NegotiateEnd() //���� �� ȣ��
    {
        //������ ... ��ŭ ����
        PlayerData.Instance.Money += GameData.Instance.RewardDic[LevelManager.Instance.Level + Define.BOSS_INDEX].money / 2;
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[LevelManager.Instance.Level + Define.BOSS_INDEX].viewers / 2;

        //���� ī�� UI ���� ��, TalkEnd ȣ��
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
