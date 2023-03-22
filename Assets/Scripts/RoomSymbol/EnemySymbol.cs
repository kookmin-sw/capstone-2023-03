
public class EnemySymbol : RoomSymbol
{

    public override void SymbolEncounter()
    {
        DialogUI dialog = PanelManager.Instance.ShowPanel("DialogUI").GetComponent<DialogUI>();
        dialog.ShowDialog(this);

    }

    public override void SymbolClear()
    {
        base.SymbolClear();
    }
}
