using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int index;
    public Sprite portrait;
    public string name;
    public string description;
    public int cost;
    public int damage;
    public int heal;
    public int attackCount;

    public string attackRange;
    public string attribute; //속성
    public string damageType;
    public string rarity; //레어도
    public string passive; //특수 효과?
}

public class GameDataCon : Singleton<GameDataCon>
{

    //파일 경로에 맞는 스프라이트 파일 전체가 저장된 딕셔너리
    public Dictionary<string, Sprite> SpriteDic { get; set; } = new Dictionary<string, Sprite>();

    //카드 전체가 저장된 리스트
    private List<Card> CardList { get; set; } = new List<Card>();

    //대화 로그 전체가 저장된 리스트
    private List<Dialog> DialogList { get; set; } = new List<Dialog>();
}
