using System;
using UnityEngine;

public class RoomSymbol : MonoBehaviour
{
    public int Index { get; set; }
    public Define.EventType Type { get; set; }

    //플레이어가 이벤트 심볼에 말을 걸었을 때
    public virtual void Encounter()
    {
        //대화창 UI를 열고 UI가 닫혔을 때, 이 심볼의 End가 수행됨 (콜백)
        DialogUI dialog = PanelManager.Instance.ShowPanel("DialogUI", true, End).GetComponent<DialogUI>();
        dialog.Init(Index);
    }

    //대충 이벤트 심볼의 이벤트가 끝났을 때
    public virtual void End()
    {
        AssetLoader.Instance.Destroy(gameObject);
    }
}
