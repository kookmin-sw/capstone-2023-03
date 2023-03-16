using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySymbol : EventSymbol
{

    public override void SymbolEncounter()
    {

        PanelManager.Instance.ShowPanel("DialogUI");

        
    }

    public override void SymbolClear()
    {
    }
}
