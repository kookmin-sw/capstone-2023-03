using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataStructs
{
    [System.Serializable]
    public class CardStruct
    {
        public int index;
        public string name;
        public string description; //설명
        public int cost;
        public int rarity;
        public string type; //공격, 스킬, 애청자 등
        public string attack_type; // 물리, 마법
        public string attribute; //속성
        public string target; //타겟
        public int damage; //공격력
        public int times; //횟수
        public string special; //특수효과
    }

    [System.Serializable]
    public class LineStruct
    {
        public string portrait;
        public string name;
        public string line;
    }

    [System.Serializable]
    public class SettingStruct
    {
        public bool tutorial;
        public int bgm;
        public int effect;
    }

    [System.Serializable]
    public class RewardStruct
    {
        public int viewers;
        public int money;
    }

}
