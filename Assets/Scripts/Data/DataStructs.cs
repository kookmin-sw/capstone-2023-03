using UnityEngine;

namespace DataStructs
{
    [System.Serializable]
    public class CardStruct
    {
        public int index;
        public string name;
        public string description;
        public int cost;
        public int rarity;
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
}
