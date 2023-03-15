using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySymbol : EventSymbol
{

    public override void SymbolEncounter()
    {

        PanelManager.Instance.ShowPanelOnStack("DialogUI");

        base.SymbolEncounter();
    }

    public override void SymbolClear()
    {
    }
}
