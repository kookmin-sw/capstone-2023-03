using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//대화창 UI에서 대화 내용을 가져오게 하는 역할을 담당
public class DialogManager : Singleton<DialogManager>
{
    //미리 JSON 파일에서 대사를 가져와서 딕셔너리에 저장.
    public Dictionary<int, List<Dialog>> DialogDic { get; set; } = new Dictionary<int, List<Dialog>>(); 

    //스탠딩 그림을 딕셔너리에 저장.
    public Dictionary<string, List<Sprite>> PortraitDic { get; set; } = new Dictionary<string, List<Sprite>>();

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

            for (int j = 0; j < dialogData[i]["lines"].Count; j++)
            {
                Dialog dialog = new Dialog();
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
            Dialog dialog = new Dialog();
            dialog.name = DialogDic[index][lineIndex].name?.ToString();
            dialog.line = DialogDic[index][lineIndex].line.ToString();
            return dialog;
        }
    }
}
