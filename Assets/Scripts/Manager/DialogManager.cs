using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;


//LitJson 플러그인 깔아야함
//1. 대화창이 대화 내용을 가져오게 하는 역할.
//2. 대화창 UI를 소환하는 함수.

public class DialogManager : Singleton<DialogManager>
{
    private DialogUI dialogUI;

    //미리 JSON 파일에서 대사를 가져와서 딕셔너리에 저장.
    private Dictionary<int, List<Dialog>> DialogDic { get; set; } = new Dictionary<int, List<Dialog>>(); 

    //이름에 맞는 스탠딩 그림을 딕셔너리에 저장.
    private Dictionary<string, Sprite> PortraitDic { get; set; } = new Dictionary<string, Sprite>();

    //게임 내에서 계속 켜져있어야 하므로 싱글톤
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        //JSON 데이터를 가져와서, 딕셔너리에 저장.
        //Index별로 구분하여 저장해서 나중에 Index로 대화 내용을 가져오게 함.
        JsonData dialogData = DataManager.Instance.LoadJson("Dialog");

        for (int i = 0; i < dialogData.Count; i++)
        {
            List<Dialog> dialogList = new List<Dialog>();

            int index = int.Parse(dialogData[i]["index"].ToString());

            //특정 줄의 대화를 딕셔너리에 추가
            for (int j = 0; j < dialogData[i]["lines"].Count; j++)
            {
                Dialog dialog = new Dialog();

                Sprite portrait;
                string portraitName = dialogData[i]["lines"][j]["portrait"]?.ToString();

                //포트레이트 이름에 맞는 초상화 파일 가져와서 저장
                if (portraitName != null)
                {
                    if (!PortraitDic.ContainsKey(portraitName))
                    {
                        portrait = AssetLoader.Instance.Load<Sprite>($"Images/Portrait/{portraitName}");
                        PortraitDic[portraitName] = portrait;
                    }
                    dialog.portrait = PortraitDic[portraitName];
                }
                else
                {
                    dialog.portrait = null;
                }

                //이름, 대사 등 가져와서 저장
                dialog.name = dialogData[i]["lines"][j]["name"]?.ToString();
                dialog.line = dialogData[i]["lines"][j]["line"].ToString();
                dialogList.Add(dialog);
            }

            DialogDic.Add(index, dialogList);
        }
    }

    //대화 딕셔너리에서 특정 인덱스의, 몇번째 줄에 해당하는 대사를 가져온다.
    public Dialog GetLine(int index, int lineIndex)
    {
        if (lineIndex == DialogDic[index].Count)
        {
            return null;
        }
        else
        {
            return DialogDic[index][lineIndex];
        }
    }

    //대화창 열기
    public void OpenDialog(int index, Action CloseCallback = null)
    {
        dialogUI = PanelManager.Instance.ShowPanel("DialogUI", false, CloseCallback).GetComponent<DialogUI>();
        dialogUI.FirstDialog(index);
    }
}
