using System;
using UnityEngine;

public class EventSymbol : MonoBehaviour
{
    Room room;

    //이거 나중에 수정해야함. 
    private void OnDisable()
    {
        SymbolClear();
    }

    //플레이어가 이벤트 심볼에 말을 걸었을 때
    public virtual void SymbolEncounter()
    {
        gameObject.SetActive(false);
    }

    //대충 이벤트 심볼의 이벤트가 끝났을 때
    public virtual void SymbolClear()
    {

    }
}
