using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    private string nextScene = null;

    [SerializeField]
    public Image progressBar; //로딩 게이지

    public void Init(string sceneName)
    {
        nextScene = sceneName;
        StartCoroutine(LoadProcess());
    }

    IEnumerator LoadProcess()
    {
        yield return null; //처음 코루틴 실행한 프레임에는 리턴

        // 비동기 로딩 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);

        // 로딩이 완료될 때까지 기다림
        while (!asyncLoad.isDone)
        {
            // 진행률을 계산하고 progressBar 이미지의 fillAmount 속성에 할당
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.fillAmount = progress/2;

            // 다음 프레임까지 기다림
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
    }
}
