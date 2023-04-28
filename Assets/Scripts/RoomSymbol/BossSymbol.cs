using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSymbol : RoomSymbol
{
    public override void TalkStart()
    {
        base.TalkStart();
    }

    //보스 클리어 시, 레벨 클리어 이벤트 실행
    public override void TalkEnd()
    {
        base.TalkEnd();

        MapManager.Instance.OnLevelClear();
    }
}
