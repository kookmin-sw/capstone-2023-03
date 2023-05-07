using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        SoundManager.Instance.Play("Sounds/BattleBgm", Sound.Bgm);
        UIManager.Instance.ShowUI("BattleUI");
    }
}
