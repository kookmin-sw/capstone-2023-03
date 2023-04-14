using System.Collections.Generic;


//다른 클래스들에서는 playData에 접근해서 저장된 데이터를 사용하거나, 정보가 변화 시(혹은 세이브 시도 시) playData를 실시간으로 갱신함.
public class PlayerData : Singleton<PlayerData>
{
    public int channelLevel;
    public List<CardStruct> playerDeck;

    protected override void Awake()
    {
        base.Awake();
    }
}
