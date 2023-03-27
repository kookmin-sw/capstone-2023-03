using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//나중에 최적화를 더 할 수 있기 때문에 일단 이 클래스를 한번 통해서 불러오게 한다
//에셋/프리팹을 불러올 때 사용 ㄱㄱ
public class AssetLoader : Singleton<AssetLoader>
{
    private Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }

    //캐시를 해서 여러번 불러오지 않도록 함
    public GameObject Load(string path)
    {
        if (!cache.ContainsKey(path))
        {
            cache.Add(path, Resources.Load<GameObject>(path));
        }

        return cache[path];
    }

    //프리팹을 (안불러왔으면 불러오고) 복제해서 소환
    //AssetLoader.Instance.Instantiate("Prefabs/BossDoor"); 식으로 사용
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load(path);
        GameObject go = Object.Instantiate(prefab, parent);

        go.name = prefab.name;

        return go;
    }

    //다른 버전
    public GameObject Instantiate(string path, Vector3 postion, Quaternion rotation, Transform parent = null)
    {
        GameObject go = Instantiate(path, parent);

        go.transform.position = postion;
        go.transform.rotation = rotation;

        return go;
    }

    //게임 오브젝트 파괴
    public void Destroy(GameObject go)
    {
        Object.Destroy(go);
    }


}
