using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    string nextScene = null;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    //로딩창 부르고, 다음 씬 로드하는 코루틴 실행하는 함수
    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;

        StartCoroutine(LoadProcess());
        SceneManager.LoadScene("LoadingScene");
    }

    //로딩 과정
    IEnumerator LoadProcess()
    {
        Debug.Log("2");
        yield return null;
        Debug.Log("3");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("4");
        SceneManager.LoadScene(nextScene);
        yield return null;
    }
}
