using System;
using UnityEngine;

public class RoomSymbol : MonoBehaviour
{
    Room room;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")    
        {
            SymbolEncounter();
            gameObject.SetActive(false);
        }
    }

    //이거 나중에 수정해야함. 
    private void OnDisable()
    {
        SymbolClear();
    }

    //플레이어가 이벤트 심볼에 말을 걸었을 때
    public virtual void SymbolEncounter()
    {

    }

    //대충 이벤트 심볼의 이벤트가 끝났을 때
    public virtual void SymbolClear()
    {

    }
}
