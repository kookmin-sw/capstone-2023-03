/*using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;


//LitJson 플러그인 깔아야함
//대화 내용이 저장된 클래스
//LineData: 한 줄의 대화 내용이 저장된 클래스
//DialogData: JSON에서 가져온, 전체 대화 내용이 저장됨. UI 등에서 접근할 수 있게 함.

public class LineData
{
    public Sprite portrait;
    public string name;
    public string currentLine;

    public LineData(Sprite portrait, string name, string currentLine)
    {
        this.portrait = portrait;
        this.name = name;
        this.currentLine = currentLine;
    }
}

public class DialogData : Singleton<DialogData>
{
    //미리 JSON 파일에서 대사를 가져와서 딕셔너리에 저장.
    //I/O를 그때그떄 하는 건 시간 소모가 크므로, 메모리에 전부 올려놓고 사용
    private Dictionary<int, List<LineData>> DialogDic { get; set; } = new Dictionary<int, List<LineData>>(); 

    //게임 내에서 계속 켜져있어야 함.
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        //JSON 데이터를 가져와서, 딕셔너리에 저장.
        //Index별로 구분하여 저장해서 나중에 Index로 대화 내용을 가져오게 함.
        JsonData dialogData = DataManager.Instance.LoadJson("LineData");

        for (int i = 0; i < dialogData.Count; i++)
        {
            List<LineData> dialogList = new List<LineData>();

            //index에 맞는 전체 대화 내용 데이터를 가져오기
            int index = int.Parse(dialogData[i]["index"].ToString());

            //특정 줄의 대화를 딕셔너리에 추가
            for (int j = 0; j < dialogData[i]["lines"].Count; j++)
            {
                Sprite portrait;
                string portraitName = dialogData[i]["lines"][j]["portrait"]?.ToString();

                //포트레이트 이름에 맞는 초상화 파일 가져와서 저장
                if (portraitName != null)
                {
                    if (!SpriteData.SpriteDictionary.ContainsKey(portraitName))
                    {
                        portrait = AssetLoader.Instance.Load<Sprite>($"Images/Portrait/{portraitName}");
                        SpriteData.SpriteDictionary[portraitName] = portrait;
                    }
                    portrait = SpriteData.SpriteDictionary[portraitName];
                }
                else
                {
                    portrait = null;
                }

                //이름, 대사 등 가져와서 저장
                string name = dialogData[i]["lines"][j]["name"]?.ToString();
                string currentLine = dialogData[i]["lines"][j]["currentLine"].ToString();

                //전체 대화 리스트에 한 줄 추가
                dialogList.Add(new LineData(portrait, name, currentLine));
            }
            DialogDic.Add(index, dialogList);
        }
    }

    //대화 딕셔너리에서 특정 인덱스의, 몇번째 줄에 해당하는 대사를 가져온다.
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
*/