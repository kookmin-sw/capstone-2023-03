using System.Collections.Generic;
using UnityEngine;


//구버전 맵 생성기 (일단 완벽히 고치기 전까지 놔둠)
public class Map : MonoBehaviour
{
    private int stage = 3;
    private int roomCount; //맵의 방 숫자
    private int roomInterval = 2; //맵의 방 사이 간격
    
    private Vector2 roomSize = new Vector2(10, 10); //현재 맵의 방 크기;
    
    private List<Room> mapRooms; //맵에 있는 방들의 배열
    private List<Vector2> mapRoomPoints; 
    private List<List<int>> mapRoomEdges;
    private Dictionary<int, Define.EventType> specialRoomIndexes; //특수 방이 될 배열 인덱스들 모음

    private void Awake()
    {
        CreateMap();
    }

    private void OnEnable()
    {
        MapManager.Instance.LevelClear += CreateMap;
    }

    public void OnDisable()
    {
        MapManager.Instance.LevelClear -= CreateMap;
    }

    //레벨에 맞게 맵 생성
    public void CreateMap()
    {
        //이전 스테이지 파괴
        if (stage != 0)
        {
            for (int i = 0; i < mapRooms.Count; i++)
            {
                AssetLoader.Instance.Destroy(mapRooms[i].gameObject);
            }
            mapRooms = null;
            mapRoomPoints = null;
            mapRoomEdges = null;
            specialRoomIndexes = null;
        }

        stage += 1;

        //스테이지 생성
        if (stage < 4)
        {
            roomCount = stage + 11;
            CreateSpecialRoomIndexes(); //특별한 방의 위치 지정
            CreateMapRooms(); //방 생성 후 방 유형 지정
            CreateMapRoomPointsAndEdges(); //방들의 위치와 연결상태를 나타낸 그래프 생성
            PlaceMapRooms(); // 그래프대로 방 위치를 배치함
        }
        else if (stage == 4)
        {
            //최종보스
            roomCount = 1;
            CreateMapRooms(); //방 생성 후 방 유형 지정
            CreateMapRoomPointsAndEdges(); //방들의 위치와 연결상태를 나타낸 그래프 생성
            PlaceMapRooms(); // 그래프대로 방 위치를 배치함
        }
        else
        {
            //엔딩 씬 호출
            Debug.Log("클리어!");
        }
    }

    //특별 맵 인덱스들 생성
    private void CreateSpecialRoomIndexes()
    {
        int minRoomIndex = 1;
        int maxRoomIndex = roomCount - 1;

        specialRoomIndexes = new Dictionary<int, Define.EventType>();

        List<int> uniqueIndexes = Define.GenerateRandomNumbers(minRoomIndex, maxRoomIndex, 5);

        // 상점의 위치를 저장
        specialRoomIndexes[uniqueIndexes[0]] = Define.EventType.Shop;

        // 휴식의 위치를 저장
        specialRoomIndexes[uniqueIndexes[1]] = Define.EventType.Rest;

        // 이벤트의 위치를 저장
        specialRoomIndexes[uniqueIndexes[2]] = Define.EventType.Event;
        specialRoomIndexes[uniqueIndexes[3]] = Define.EventType.Event;
    }

    //특정 번호의 방이 무슨 방인지 타입을 리턴
    Define.EventType SelectRoomType(int node)
    {
        if (node == roomCount - 1) return Define.EventType.Boss;
        if (node == 0) return Define.EventType.Start;

        //특별한 방이 있나 확인 후 가져오기
        if (specialRoomIndexes.ContainsKey(node))
        {
            return specialRoomIndexes[node];
        }

        return Define.EventType.Enemy;
    }

    //맵의 방을 저장하는 Rooms 배열 생성
    private void CreateMapRooms()
    {
        mapRooms = new List<Room>(roomCount);

        for (int node = 0; node < roomCount; node++)
        {
            //방 타입 고르기
            Define.EventType roomType = SelectRoomType(node);

            //방 게임오브젝트 생성
            Room currentRoom = AssetLoader.Instance.Instantiate($"Prefabs/Room/Room{stage}", transform).AddComponent<Room>();
            currentRoom.name = $"Room{node}";

            //멤버 변수 초기화
            currentRoom.Init(roomType);

            //배열에 추가
            mapRooms.Add(currentRoom);
        }

        if (stage == 4) { mapRooms[0].Symbol.transform.position += new Vector3(-1.5f, 0, 1.5f); }
    }

    //큐를 이용해서 방의 위치와 연결 관계를 나타낸 그래프를 생성하는 bfs 변형 함수
    void CreateMapRoomPointsAndEdges()
    {
        int currentRoomCount = 1;
        int currentRoomIndex = 0;

        mapRoomPoints = new List<Vector2>(roomCount);
        mapRoomEdges = InitializeMapRoomEdges();
        Queue<Vector2> roomQueue = new Queue<Vector2>();
        HashSet<Vector2> visitedRoomPoints = new HashSet<Vector2>(roomCount);

        //시작점 추가
        mapRoomPoints.Add(Vector2.zero);
        visitedRoomPoints.Add(Vector2.zero);
        roomQueue.Enqueue(Vector2.zero);

        //맵 그래프 생성
        while (currentRoomCount < roomCount)
        {
            //방 위치 가져오기
            Vector2 currentRoomPoint = roomQueue.Dequeue();

            //다음으로 탐색할 랜덤한 수만큼의 랜덤한 방향 생성.
            int nearRoomCount = Random.Range(1, 5);
            List<int> nearRoomDirections = Define.GenerateRandomNumbers(0, 4, nearRoomCount);

            foreach (int dir in nearRoomDirections)
            {
                //방을 다 채웠을 경우 종료
                if (currentRoomCount >= roomCount) break;

                //방향에 대응하는 벡터값을 더해 새로 탐색할 방의 벡터값 리턴
                Vector2 newRoomPoint = currentRoomPoint + Define.directionVectors[(Define.Direction)dir];

                //방문하지 않은 곳일 경우 방문 처리
                if (!visitedRoomPoints.Contains(newRoomPoint))
                {
                    visitedRoomPoints.Add(newRoomPoint);
                    roomQueue.Enqueue(newRoomPoint);
                    currentRoomCount++;

                    //방 위치, 해당 방과 연결된 다른 방 추가
                    //roomEdges는 [인덱스1: 방의 번호, 인덱스2: 방향] 에다가 그 방향에 [연결된 다른 방의 인덱스]를 저장
                    mapRoomPoints.Add(newRoomPoint);
                    mapRoomEdges[currentRoomIndex][dir] = currentRoomCount - 1;
                    mapRoomEdges[currentRoomCount - 1][(dir + 2) % 4] = currentRoomIndex;
                }
            }

            //드물게 갈 길이 막혔거나, 운이 좋지 않아서 방문한 곳에만 다시 방문한 경우
            //방을 다 못채웠는데 큐가 비는 경우가 생긴다. 이때 지금까지 생성한 방 중 랜덤한 위치를 큐에 넣고 다시 탐색을 실시.
            //아니면 방을 생성하고 배열에 넣기
            if (roomQueue.Count == 0 && currentRoomCount < roomCount)
            {
                int nextRoomIndex = Random.Range(0, mapRoomPoints.Count);
                currentRoomIndex = nextRoomIndex;
                roomQueue.Enqueue(mapRoomPoints[nextRoomIndex]);
            }
            else
            {
                currentRoomIndex++;
            }
        }
    }

    //실제 Room 오브젝트의 위치 조정 + 문 연결
    void PlaceMapRooms()
    {
        for (int node = 0; node < mapRooms.Count; node++)
        {
            //mapRoomPoints의 좌표에 맞게 mapRooms에 속한 Room 오브젝트의 위치 조정
            mapRooms[node].transform.position = new Vector3(
                mapRoomPoints[node].x * (roomSize.x + roomInterval),
                0,
                mapRoomPoints[node].y * (roomSize.y + roomInterval)
            );
        }

        for (int node = 0; node < mapRooms.Count; node++)
        {
            //Room마다 연결된 방이 있는지 확인
            for (int dir = 0; dir < mapRoomEdges[node].Count; dir++)
            {
                //해당 방향에 연결된 방이 있는 경우
                if (mapRoomEdges[node][dir] != -1)
                {
                    //서로를 잇는 문 추가
                    mapRooms[node].SetDoorsDictionary((Define.Direction)dir, mapRooms[mapRoomEdges[node][dir]]);
                }
            }
        }
    }

    //2차원 RoomEdges 배열 초기화
    List<List<int>> InitializeMapRoomEdges()
    {
        int dir = (int)Define.Direction.Count;

        List<List<int>> roomEdges = new List<List<int>>(roomCount * dir);

        for (int i = 0; i < roomCount; i++)
        {
            roomEdges.Add(new List<int>(dir));

            for (int j = 0; j < dir; j++)
            {
                roomEdges[i].Add(-1);
            }
        }
        return roomEdges;
    }
}
