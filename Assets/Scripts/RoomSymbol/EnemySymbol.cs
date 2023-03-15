using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySymbol : RoomSymbol
{

    public override void SymbolEncounter()
    {
        base.SymbolEncounter();

        Debug.Log("¿˚ µÓ¿Â!");
    }

    public override void SymbolClear()
    {
    }
}
