using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleWinUI : MonoBehaviour
{
    [SerializeField]
    private Button ReturnButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void BackButtonClick()
    {
        UIManager.Instance.HideUI("BattleWinUI");
        UIManager.Instance.HideUI("BattleUI");
        UIManager.Instance.HideUI("BackGroundUI");

        GameObject mainCameraParent = GameObject.Find("PlayerCameraParent");
        GameObject battleCameraParent = GameObject.Find("BattleCameraParent");
        if (battleCameraParent != null)
        {
            mainCameraParent.transform.GetChild(0).gameObject.SetActive(true);
            battleCameraParent.transform.GetChild(0).gameObject.SetActive(false);
        }
        SoundManager.Instance.Play("Sounds/StageBgm", Sound.Bgm);
    }

    
        
}
