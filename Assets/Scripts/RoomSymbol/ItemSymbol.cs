using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSymbol : EventSymbol
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
