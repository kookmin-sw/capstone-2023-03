using UnityEngine;

public class EnemySymbol : RoomSymbol
{

    //EnemySymbol Index  Theme 踰몄 媛.


    public override void TalkStart()
    {
        UIManager.Instance.ShowUI("DialogUI")
        .GetComponent<DialogUI>()
        .Init(index, SelectOpen); //媛 硫   ㅽ
    }

    //  ㅽ
    public void SelectOpen()
    {
        UIManager.Instance.ShowUI("SelectUI")
            .GetComponent<SelectUI>()
            .Init(
                "寃듬源?",
                TryNegotiate, //     ⑥ 몄
                () => {
                    UIManager.Instance.ShowUI("DialogUI")
                    .GetComponent<DialogUI>()
                    .Init(index + Define.FIGHT_INDEX, Fight);
                } //   �   � UI 몄. 以 닿구 Fight ⑥濡 �.
            );
    }

    // 
    public void TryNegotiate()
    {
        float random = Random.Range(0f, 1f);

        // 怨瑜대㈃ ㅽ 瑜濡  , ㅽ⑦硫  ㅽ  몄  � 몄
        if (random < 0.5f && StageManager.Instance.NegoInLevel == false) //쇰 30 瑜 +  ㅽ댁   � 쇰㈃  깃났
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
        //� UI 닿린
        SceneLoader.Instance.LoadScene("BattleScene");
        //� UI レ , FightEnd 몄
    }

    public void FightEnd() //�   몄
    {

        //濡 �  泥대 
        PlayerData.Instance.CurrentHp -= 5;

        //ㅽ�  �踰⑥ 留 蹂댁 留 利媛
        PlayerData.Instance.Money += GameData.Instance.RewardDic[StageManager.Instance.Stage].money; // �踰⑥ 대뱁 蹂댁 媛�몄, ㅽ� 異媛
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[StageManager.Instance.Stage].viewers;

        //蹂댁 移대 UI レ , TalkEnd 몄
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.Init(TalkEnd);
        cardSelectUI.BattleReward();
    }

    public void NegotiateEnd() // 깃났  몄
    {
        //ㅽ�  �踰⑥ 留 蹂댁/2 留 利媛
        PlayerData.Instance.Money += GameData.Instance.RewardDic[StageManager.Instance.Stage].money / 2;
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[StageManager.Instance.Stage].viewers / 2;

        //蹂댁 移대 UI レ , TalkEnd 몄
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.Init(TalkEnd);
        cardSelectUI.NegoReward(index); // 몃깆ㅼ 留 〓す 猷 移대 
    }

    public override void TalkEnd()
    {
        base.TalkEnd();
        if (PlayerData.Instance.CheckLevelUp()) //�踰⑥  寃쎌곗
        {
            UIManager.Instance.ShowUI("CardSelectUI")
                .GetComponent<CardSelectUI>()
                .LevelUpReward();
        }
    }
}