using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//레벨의 구성 요소들, 레벨의 특정 시점에서 실행될 이벤트를 싱글톤으로 저장
//이거랑 맵은 좀 수정이 많이 들어갈 수 있다
public class LevelManager : Singleton<LevelManager>
{
    public Map Map { get; set; }
    public Room CurrentRoom { get; set; }
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

    public void OnRoomClear()
    {
        onRoomClear?.Invoke(CurrentRoom);
    }

    //델리게이트가 참조한 함수들에게 레벨이 클리어되었음을 알리는 함수 -> 전부 실행
    public void OnLevelClear()
    {
        onLevelClear?.Invoke();
    }
}
