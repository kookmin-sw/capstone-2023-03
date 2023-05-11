using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Canvas의 카메라를 BattleCamera로 설정, 그런 카메라가 없다면 메인 카메라로 설정
        Canvas canvas = GetComponent<Canvas>();
        Camera battleCamera = GameObject.Find("BattleCameraParent").transform.GetChild(0).GetComponent<Camera>();
        Camera mainCamera = Camera.main;
        if (battleCamera != null)
        {
            canvas.worldCamera = battleCamera;
            mainCamera.gameObject.SetActive(false);
            battleCamera.gameObject.SetActive(true);
        }
        else
        {
            canvas.worldCamera = mainCamera;
        }
    }

    // Update is called once per frame
}
