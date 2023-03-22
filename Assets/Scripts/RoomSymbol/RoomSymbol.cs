using System;
using UnityEngine;

public class RoomSymbol : MonoBehaviour
{
    public int Index { get; set; }
    public Define.EventType SymbolType { get; set; }

    //플레이어가 이벤트 심볼에 말을 걸었을 때
    public virtual void SymbolEncounter()
    {

    }

    //대충 이벤트 심볼의 이벤트가 끝났을 때
    public virtual void SymbolClear()
    {
        
    }
}
