
using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public int index;
    public string name;
    public string description;
    public int cost;
}

[System.Serializable]
public class LineData
{
    public string portrait;
    public string name;
    public string line;
}

//대충 게임에서 사용하기 위해 불러와야할 데이터들 모임. 전체 카드, 전체 대화 목록, 전체 스프라이트 이미지 목록 등
public class GameDataCon : Singleton<GameDataCon>
{

    //파일 경로에 맞는 스프라이트 파일 전체가 저장된 딕셔너리
    //굳이 로드하는 이유는 I/O 줄이려고
    public Dictionary<string, Sprite> SpriteDic { get; set; } = new Dictionary<string, Sprite>();

    //카드 전체가 저장된 리스트
    public List<CardData> CardList { get; set; } = new List<CardData>();

    //대화 로그 전체가 저장된 리스트
    public Dictionary<int, List<LineData>> DialogDic { get; set; } = new Dictionary<int, List<LineData>>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        LoadSpriteDic();
        LoadCardList();
        LoadDialogDic();
    }




    //스프라이트 전부 로드
    public void LoadSpriteDic()
    {
        Sprite[] sprites = AssetLoader.Instance.LoadAll<Sprite>("Images/Portrait");
        foreach (Sprite sprite in sprites)
        {
            SpriteDic.Add(sprite.name, sprite);
        }
    }

    //카드 리스트 로드
    public void LoadCardList()
    {
        string filePath = "Assets/Resources/Data/CardLibrary.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            CardList = JsonMapper.ToObject<List<CardData>>(jsonData);
        }
    }

    //대화 로그 전체 로드
    public void LoadDialogDic()
    {
        string filePath = "Assets/Resources/Data/Dialog.json";
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            JsonData dialogData = JsonMapper.ToObject(jsonString);
            for(int i = 0; i < dialogData.Count; i++)
            {
                int index = (int)dialogData[i]["index"];
                List<LineData> lines = new List<LineData>();

                for(int j = 0; j < dialogData[i]["lines"].Count; j++)
                {
                    LineData line = new LineData();
                    line.portrait = dialogData[i]["lines"][j]["portrait"]?.ToString();
                    line.name = dialogData[i]["lines"][j]["name"]?.ToString();
                    line.line = dialogData[i]["lines"][j]["line"].ToString();
                    lines.Add(line);
                }
                DialogDic.Add(index, lines);   
            }
        }
    }

    //대화 목록에서 한 줄 불러오기
    public LineData GetLine(int index, int lineIndex)
    {
        if (lineIndex >= DialogDic[index].Count)
        {
            return null;
        }
        else
        {
            return DialogDic[index][lineIndex];
        }
    }
}
