using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSymbol : RoomSymbol
{
    public override void Encounter()
    {
        Debug.Log("ItemRoom");
    }

    public override void Clear()
    {
    }
}
