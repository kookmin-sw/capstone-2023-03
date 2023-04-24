using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//�� �迭��, ���� Ư�� �������� ����� �̺�Ʈ�� �̱������� ����
public class MapManager : Singleton<MapManager>
{
    private int stage = 0;
    private int roomCount; //���� �� ����
    private int roomInterval = 2; //���� �� ���� ����
    private Vector2 roomSize = new Vector2(10, 10); //���� ���� �� ũ��
    private Dictionary<int, Define.EventType> specialRoomIndexes; //Ư�� ���� �� �迭 �ε����� ����

    public Room CurrentRoom { get; set; }
    public event Action<Room> RoomClear;
    public event Action LevelClear;

    public List<Room> Rooms { get; set; }   
    public List<Vector2> RoomPoints { get; set; }
    public List<List<int>> RoomEdges { get; set; }

    //���� ��ġ�� �θ� ���ӿ�����Ʈ
    public GameObject Map
    {
        get
        {
            GameObject map = GameObject.Find("Map");
            if (map == null)
            {
                map = new GameObject();
                map.name = "Map";
            }
            return map;
        }
    }

    //��������Ʈ�� ������ �Լ��鿡�� ���� ���� Ŭ����Ǿ����� �˸��� �Լ�. �̰� �ε����� �ٲ���.
    public void OnRoomClear()
    {
        RoomClear?.Invoke(CurrentRoom);
    }

    //������ Ŭ����Ǿ��� �� �����ϴ� �Լ�. ��������Ʈ�� ������ �Լ��鿡�� Ŭ����Ǿ����� �˸�
    public void OnLevelClear()
    {
        DestroyMap();
        CreateMap();
        LevelClear?.Invoke();
    }

    //���� �� �ı�
    public void DestroyMap()
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            AssetLoader.Instance.Destroy(Rooms[i].gameObject);
        }
        Rooms = null;
        RoomPoints = null;
        RoomEdges = null;
        specialRoomIndexes = null;
    }

    //�� ����
    public void CreateMap()
    {
        stage += 1;

        //�������� ����
        if (stage < 4)
        {
            roomCount = stage + 11;
            CreateSpecialRoomIndexes(); //Ư���� ���� ��ġ ����
            CreateMapRooms(); //�� ���� �� �� ���� ����
            CreateMapRoomPointsAndEdges(); //����� ��ġ�� ������¸� ��Ÿ�� �׷��� ����
            PlaceMapRooms(); // �׷������ �� ��ġ�� ��ġ��
        }
        else if (stage == 4)
        {
            //��������
            roomCount = 1;
            CreateMapRooms(); //�� ���� �� �� ���� ����
            CreateMapRoomPointsAndEdges(); //����� ��ġ�� ������¸� ��Ÿ�� �׷��� ����
            PlaceMapRooms(); // �׷������ �� ��ġ�� ��ġ��
        }
        else
        {
            //���� �� ȣ��
            Debug.Log("Ŭ����!");
        }
    }

    //Ư�� ���� ����� �ε����� ����
    private void CreateSpecialRoomIndexes()
    {
        int minRoomIndex = 1;
        int maxRoomIndex = roomCount - 1;

        specialRoomIndexes = new Dictionary<int, Define.EventType>();

        List<int> uniqueIndexes = Define.GenerateRandomNumbers(minRoomIndex, maxRoomIndex, 5);

        // ������ ��ġ�� ����
        specialRoomIndexes[uniqueIndexes[0]] = Define.EventType.Shop;

        // �޽��� ��ġ�� ����
        specialRoomIndexes[uniqueIndexes[1]] = Define.EventType.Rest;

        // �̺�Ʈ�� ��ġ�� ����
        specialRoomIndexes[uniqueIndexes[2]] = Define.EventType.Event;
        specialRoomIndexes[uniqueIndexes[3]] = Define.EventType.Event;
    }

    //Ư�� �ε����� ���� ���� ������ Ÿ���� ����
    Define.EventType SelectRoomType(int node)
    {
        if (node == roomCount - 1) return Define.EventType.Boss;
        if (node == 0) return Define.EventType.Start;

        //Ư���� ���� �ֳ� Ȯ�� �� ��������
        if (specialRoomIndexes.ContainsKey(node))
        {
            return specialRoomIndexes[node];
        }

        return Define.EventType.Enemy;
    }

    //���� ���� �����ϴ� Rooms �迭 ����
    private void CreateMapRooms()
    {
        Rooms = new List<Room>(roomCount);

        for (int node = 0; node < roomCount; node++)
        {
            //�� Ÿ�� ������
            Define.EventType roomType = SelectRoomType(node);

            //�� ���ӿ�����Ʈ ����
            Room currentRoom = AssetLoader.Instance.Instantiate($"Prefabs/Room/Room{stage}", Map.transform).AddComponent<Room>();
            currentRoom.name = $"Room{node}";

            //��� ���� �ʱ�ȭ
            currentRoom.Init(roomType);

            //�迭�� �߰�
            Rooms.Add(currentRoom);
        }

        if (stage == 4) { Rooms[0].Symbol.transform.position += new Vector3(-1.5f, 0, 1.5f); }
    }

    //ť�� �̿��ؼ� ���� ��ġ�� ���� ���踦 ��Ÿ�� �׷����� �����ϴ� bfs ���� �Լ�
    void CreateMapRoomPointsAndEdges()
    {
        int currentRoomCount = 1;
        int currentRoomIndex = 0;

        RoomPoints = new List<Vector2>(roomCount);
        RoomEdges = InitializeMapRoomEdges();
        Queue<Vector2> roomQueue = new Queue<Vector2>();
        HashSet<Vector2> visitedRoomPoints = new HashSet<Vector2>(roomCount);

        //������ �߰�
        RoomPoints.Add(Vector2.zero);
        visitedRoomPoints.Add(Vector2.zero);
        roomQueue.Enqueue(Vector2.zero);

        //�� �׷��� ����
        while (currentRoomCount < roomCount)
        {
            //�� ��ġ ��������
            Vector2 currentRoomPoint = roomQueue.Dequeue();

            //�������� Ž���� ������ ����ŭ�� ������ ���� ����.
            int nearRoomCount = Random.Range(1, 5);
            List<int> nearRoomDirections = Define.GenerateRandomNumbers(0, 4, nearRoomCount);

            foreach (int dir in nearRoomDirections)
            {
                //���� �� ä���� ��� ����
                if (currentRoomCount >= roomCount) break;

                //���⿡ �����ϴ� ���Ͱ��� ���� ���� Ž���� ���� ���Ͱ� ����
                Vector2 newRoomPoint = currentRoomPoint + Define.directionVectors[(Define.Direction)dir];

                //�湮���� ���� ���� ��� �湮 ó��
                if (!visitedRoomPoints.Contains(newRoomPoint))
                {
                    visitedRoomPoints.Add(newRoomPoint);
                    roomQueue.Enqueue(newRoomPoint);
                    currentRoomCount++;

                    //�� ��ġ, �ش� ��� ����� �ٸ� �� �߰�
                    //roomEdges�� [�ε���1: ���� ��ȣ, �ε���2: ����] ���ٰ� �� ���⿡ [����� �ٸ� ���� �ε���]�� ����
                    RoomPoints.Add(newRoomPoint);
                    RoomEdges[currentRoomIndex][dir] = currentRoomCount - 1;
                    RoomEdges[currentRoomCount - 1][(dir + 2) % 4] = currentRoomIndex;
                }
            }

            //�幰�� �� ���� �����ų�, ���� ���� �ʾƼ� �湮�� ������ �ٽ� �湮�� ���
            //���� �� ��ä���µ� ť�� ��� ��찡 �����. �̶� ���ݱ��� ������ �� �� ������ ��ġ�� ť�� �ְ� �ٽ� Ž���� �ǽ�.
            //�ƴϸ� ���� �����ϰ� �迭�� �ֱ�
            if (roomQueue.Count == 0 && currentRoomCount < roomCount)
            {
                int nextRoomIndex = Random.Range(0, RoomPoints.Count);
                currentRoomIndex = nextRoomIndex;
                roomQueue.Enqueue(RoomPoints[nextRoomIndex]);
            }
            else
            {
                currentRoomIndex++;
            }
        }
    }

    //���� Room ������Ʈ�� ��ġ ���� + �� ����
    void PlaceMapRooms()
    {
        for (int node = 0; node < Rooms.Count; node++)
        {
            //mapRoomPoints�� ��ǥ�� �°� mapRooms�� ���� Room ������Ʈ�� ��ġ ����
            Rooms[node].transform.position = new Vector3(
                RoomPoints[node].x * (roomSize.x + roomInterval),
                0,
                RoomPoints[node].y * (roomSize.y + roomInterval)
            );
        }

        for (int node = 0; node < Rooms.Count; node++)
        {
            //Room���� ����� ���� �ִ��� Ȯ��
            for (int dir = 0; dir < RoomEdges[node].Count; dir++)
            {
                //�ش� ���⿡ ����� ���� �ִ� ���
                if (RoomEdges[node][dir] != -1)
                {
                    //���θ� �մ� �� �߰�
                    Rooms[node].SetDoorsDictionary((Define.Direction)dir, Rooms[RoomEdges[node][dir]]);
                }
            }
        }
    }

    //2���� RoomEdges �迭 �ʱ�ȭ
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