
public class EnemySymbol : RoomSymbol
{

    public override void TalkStart()
    {
        UIManager.Instance.ShowUI("DialogUI")
        .GetComponent<DialogUI>()
        .Init(Index, SelectOpen);

    }

    //협상 팝업 오픈
    public void SelectOpen()
    {
        UIManager.Instance.ShowUI("SelectUI")
            .GetComponent<SelectUI>()
            .Init(
                "협상하시겠습니까?", 
                () => { UIManager.Instance.ShowUI("DialogUI").GetComponent<DialogUI>().Init(Index + 6000, TalkEnd); }, //협상 대화 후 카드, 보상 획득
                () => { UIManager.Instance.ShowUI("DialogUI").GetComponent<DialogUI>().Init(Index + 7000, TalkEnd); } //비협상 대화 후 전투 UI 호출
            );
    }

    //협상 시도
    public void TryNegotiate()
    {
        //협상 고르면 랜덤한 확률로 협상 대화, 실패하면 협상 실패 대화 호출
    }

    public void Fight()
    {
        //전투 UI 열기
        //전투 UI 닫을 시, FightEnd 호출
    }

    public void FightEnd()
    {
        //전투 끝날 시 호출
        //아이템 UI 닫을 시, TalkEnd 호출
    }

    public void Negotiate()
    {
        //협상 성공 후 카드 획득
        //아이템 UI 닫을 시, TalkEnd 호출
    }

    public override void TalkEnd()
    {
        base.TalkEnd();

    }
}
