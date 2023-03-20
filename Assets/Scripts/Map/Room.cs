using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//자동 구현 프로퍼티 사용 이유
//1. 함수라서 디버깅에 걸린다.
public class Room : MonoBehaviour
{
    private bool IsCleared { get; set; } = false;
    private Define.RoomEventType type;
    public EventSymbol RoomSymbol { get; set; } = null;

    //갖고있는 방향-문 딕셔너리
    public Dictionary<Define.Direction, Door> Doors { get; set; } = new Dictionary<Define.Direction, Door>((int)Define.Direction.Count);

    private void OnTriggerEnter(Collider collider)
    {
        if(IsCleared == false)
        { 
            //처음 들어올 때 문 비활성화
            ActivateDoors(false);

            //몬스터가 있는 방이 아니면 바로 클리어 처리
            if ((type != Define.RoomEventType.Normal && type != Define.RoomEventType.Boss) && collider.gameObject.tag == "Player")
            {
                IsCleared = true;
                ActivateDoors(true);
                GameManager.Instance.OnRoomClear();
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        //적이 있는 방이면, 승리 혹은 협상 시 적 심볼이 파괴되고? 이때 클리어 처리
        if (RoomSymbol != null && !RoomSymbol.isActiveAndEnabled && IsCleared == false)
        {
            IsCleared = true;
            ActivateDoors(true);
            GameManager.Instance.OnRoomClear();
        }
    }

    //멤버 변수 초기화
    public void Init(Define.RoomEventType type)
    {
        //Type 지정
        this.type = type;

        //RoomSymbol 소환
        switch (type)
        {
            case Define.RoomEventType.Normal:
                RoomSymbol = AssetLoader.Instance.Instantiate($"Prefabs/EventSymbol/EnemySymbol", transform).AddComponent<EnemySymbol>();
                break;
            case Define.RoomEventType.Item:
                RoomSymbol = AssetLoader.Instance.Instantiate($"Prefabs/EventSymbol/ItemSymbol", transform).AddComponent<ItemSymbol>();
                break;
            case Define.RoomEventType.Shop:
                RoomSymbol = AssetLoader.Instance.Instantiate($"Prefabs/EventSymbol/ShopSymbol", transform).AddComponent<ShopSymbol>();
                break;
            case Define.RoomEventType.Boss:
                RoomSymbol = AssetLoader.Instance.Instantiate($"Prefabs/EventSymbol/BossSymbol", transform).AddComponent<BossSymbol>();
                break;
            default:
                return;
        }
        RoomSymbol.transform.position = new Vector3(0, 1, 0);
    }

    //이 방에서 특정 방향에 있는 문을 목적지 위치와 연결하고, 소유한 Doors 딕셔너리에 추가한다.
    public void SetDoorsDictionary(Define.Direction direction, Room destination)
    {
        //일단 소유한 문 딕셔너리에 <방향-문> 추가
        Doors[direction] = transform.Find("Doors").GetChild((int)direction).GetComponent<Door>();
        Doors[direction].gameObject.SetActive(true);

        //현재 방 중심에서 문까지 가는 벡터의 반대 벡터
        Vector3 oppositeVector = (transform.position - Doors[direction].transform.position) * 0.8f;
        oppositeVector.y = 0;

        //문의 목적지가 될 벡터값 설정
        Doors[direction].Destination = destination.transform.position + oppositeVector;    
    }

    //문들 전부 활성화/비활성화
    public void ActivateDoors(bool isActivated)
    {
        foreach(KeyValuePair<Define.Direction, Door> door in Doors) 
        {
            door.Value.gameObject.SetActive(isActivated);
        }
    }

}
