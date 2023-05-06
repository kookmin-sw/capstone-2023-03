
using UnityEngine;

public class EnemySymbol : RoomSymbol
{
    //EnemySymbol���� Index�� ���� Theme�� ��ȣ�� ����.


    public override void TalkStart()
    {
        UIManager.Instance.ShowUI("DialogUI")
        .GetComponent<DialogUI>()
        .Init(index, SelectOpen); //��ȭ�� ������ ���� �˾� ����
    }

    //���� �˾� ����
    public void SelectOpen()
    {
        UIManager.Instance.ShowUI("SelectUI")
            .GetComponent<SelectUI>()
            .Init(
                "�����Ͻðڽ��ϱ�?", 
                TryNegotiate, //�� ���� �� ���� �õ� �Լ� ȣ��
                () => { UIManager.Instance.ShowUI("DialogUI").GetComponent<DialogUI>().Init(index + Define.FIGHT_INDEX, FightEnd); } //�ƴϿ� ���� �� ���� ��ȭ �� ���� UI ȣ��
            );
    }

    //���� �õ�
    public void TryNegotiate()
    {
        float random = Random.Range(0f, 1f);

        //���� ������ ������ Ȯ���� ���� ��ȭ, �����ϸ� ���� ���� ��ȭ ȣ�� �� ���� ȣ��
        if(random < 0.5f && StageManager.Instance.NegoInLevel == false) //�ϴ� 30�� Ȯ�� + �� ������������ ������ �� �� ������ ���� ����
        {
            UIManager.Instance.ShowUI("DialogUI").GetComponent<DialogUI>().Init(index + Define.NEGO_INDEX, NegotiateEnd);
        }
        else
        {
            UIManager.Instance.ShowUI("DialogUI").GetComponent<DialogUI>().Init(index + Define.NEGOFAIL_INDEX, Fight);
        }
    }

    public void Fight()
    {
        //���� UI ����
        SceneLoader.Instance.LoadScene("BattleScene");
        //���� UI ���� ��, FightEnd ȣ��
    }

    public void FightEnd() //���� ���� �� ȣ��
    {

        //�ӽ÷� ���� �� ü�� �϶�
        PlayerData.Instance.CurrentHp -= 5;

        //������ ���� ������ �´� ���� ��ŭ ����
        PlayerData.Instance.Money += GameData.Instance.RewardDic[StageManager.Instance.Stage].money; //���� ������ �ش��ϴ� ������ �����ͼ�, ���ȿ� �߰�
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[StageManager.Instance.Stage].viewers;

        //���� ī�� UI ���� ��, TalkEnd ȣ��
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.Init(TalkEnd);
        cardSelectUI.BattleReward();
    }

    public void NegotiateEnd() //���� ���� �� ȣ��
    {
        //������ ���� ������ �´� ����/2 ��ŭ ����
        PlayerData.Instance.Money += GameData.Instance.RewardDic[StageManager.Instance.Stage].money/2;
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[StageManager.Instance.Stage].viewers/2;

        //���� ī�� UI ���� ��, TalkEnd ȣ��
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.Init(TalkEnd);
        cardSelectUI.NegoReward(index); //���� �ε����� �´� ��� ���� ī�� ȹ��
    }

    public override void TalkEnd()
    {
        base.TalkEnd();
        if(PlayerData.Instance.CheckLevelUp()) //������ ���� ��쿡
        {
            UIManager.Instance.ShowUI("CardSelectUI")
                .GetComponent<CardSelectUI>()
                .LevelUpReward();
        }
    }
}
