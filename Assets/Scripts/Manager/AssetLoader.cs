using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//나중에 어드레서블로 바꿀수도 있기 때문에 일단 매니저 클래스를 한번 통해서 불러오게 한다
//캐싱을 통해 리소스 불러오는 시간 줄임
//근데 instantiate가 비용이 너무 커서 티는 안난다...만 update문에서 많이 부르거나 하면 날듯
public class AssetLoader : Singleton<AssetLoader>
{
    private Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public GameObject Load(string path)
    {
        if (!cache.ContainsKey(path))
        {
            cache.Add(path, Resources.Load<GameObject>(path));
        }

        return cache[path];
    }


}
