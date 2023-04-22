using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//레벨의 특정 시점에서 실행될 이벤트를 싱글톤으로 저장
//이거랑 맵은 좀 수정이 많이 들어갈 수 있다
public class LevelManager : Singleton<LevelManager>
{
    public Room CurrentRoom { get; set; }

    public event Action<Room> RoomCleared;

    public event Action LevelCleared;

    public List<Room> Rooms { get; set; }   
    public List<Vector2> RoomPoints { get; set; }
    public List<List<int>> RoomEdges { get; set; }

    public void OnRoomClear()
    {
        RoomCleared?.Invoke(CurrentRoom);
    }

    //델리게이트가 참조한 함수들에게 레벨이 클리어되었음을 알리는 함수 -> 전부 실행
    public void OnLevelClear()
    {
        LevelCleared?.Invoke();
    }
}
