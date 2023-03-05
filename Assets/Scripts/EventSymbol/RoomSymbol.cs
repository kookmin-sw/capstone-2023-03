using System;
using UnityEngine;

public abstract class RoomSymbol : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")    
        {
            Encounter();
            gameObject.SetActive(false);
        }
    }

    //이거 나중에 수정해야함. 게임 끌때도 이게 실행되서 오브젝트를 생성하게 됨
    private void OnDisable()
    {
        Clear();
    }

    //플레이어가 이벤트 심볼에 말을 걸었을 때
    public abstract void Encounter();

    //대충 이벤트 끝났을 때
    public abstract void Clear();
}
