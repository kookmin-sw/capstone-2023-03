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

    public Card(
        int index, 
        string name, 
        string description, 
        int cost, 
        int damage, 
        int attackCount, 
        string attackRange, 
        string attribute, 
        string damageType, 
        string rarity
    )
    {
        this.index = index;
        this.name = name;
        this.description = description;
        this.cost = cost;
        this.damage = damage;
        this.attackCount = attackCount;
        this.attackRange = attackRange;
        this.attribute = attribute;
        this.damageType = damageType;
        this.rarity = rarity;
    }
}