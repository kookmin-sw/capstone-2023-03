using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySymbol : RoomSymbol
{

    public override void SymbolEncounter()
    {
        DialogUI dialog = PanelManager.Instance.ShowPanel("DialogUI").GetComponent<DialogUI>();
        dialog.ShowDialog(Index);

    }

    public override void SymbolClear()
    {
    }
}
