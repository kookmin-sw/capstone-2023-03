using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSymbol : RoomSymbol
{
    public override void SymbolEncounter()
    {
        base.SymbolEncounter();
        Debug.Log("æ∆¿Ã≈€ »πµÊ!");
    }

    public override void SymbolClear()
    {
    }
}
