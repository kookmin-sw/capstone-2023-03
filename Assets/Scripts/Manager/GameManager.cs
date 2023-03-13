using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//레벨의 구성 요소들, 레벨의 특정 시점에서 실행될 이벤트를 싱글톤으로 저장
public class GameManager : Singleton<GameManager>
{
    public Map Map { get; set; }
    public GameObject Player { get; set; }
    public Vector3 StartPosition { get; set; } = Vector3.zero;

    public event Action<Room> onRoomClear;
    public event Action onLevelClear;

    protected override void Awake()
    {
        base.Awake();

        Map = new GameObject("Map").AddComponent<Map>();
        Player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), StartPosition, Quaternion.identity); 
    }


    //이렇게 이벤트를 글로벌로 관리한다면, 룸 클리어 이벤트 리스너들이 실행될 때, '어떤 룸이 클리어되었는지' 를 알 수 있어야
    //특정 방만 문이 열린다던지 하는 게 가능한데... 
    //딕셔너리로 어떻게 저장을?? 하면 되나?
    public void OnRoomClear(Room room)
    {
        onRoomClear?.Invoke(room);
    }

    //델리게이트가 참조한 함수들에게 레벨이 클리어되었음을 알리는 함수 -> 전부 실행
    public void OnLevelClear()
    {
        onLevelClear?.Invoke();
    }
}
