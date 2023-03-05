using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSymbol : RoomSymbol
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SymbolEncounter()
    {
        Debug.Log("ShopRoom");
    }

    public override void SymbolClear()
    {
        LevelManager.Instance.OnRoomClear();
    }
}
