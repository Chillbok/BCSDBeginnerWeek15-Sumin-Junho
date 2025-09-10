using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Pool;


public class MapCreateManager : Singleton<MapCreateManager>
{
    // 오브젝트 풀링 풀
    private IObjectPool<Map> pool;

    [Header("스크립터블 오브젝트")]
    [Tooltip("게임 툴팁")]
    [SerializeField]
    private GameManagerSO gameManagerSO;

    [Header("맵")]
    [SerializeField]
    private GameObject[] mapPrefab;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //씬이 로드되면 호출될 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == gameManagerSO.NameOfPlayScene)
        {
            pool = new ObjectPool<Map>(CreatingMap, OnGetMap, OnReleaseMap, OnDestroyMap, maxSize: 2);
            var firstMap = CreateMap();
            firstMap.transform.position = Vector3.zero + Vector3.forward * 20;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //오브젝트 풀에서 맵을 하나 가져와서 생성하고 반환
    public Map CreateMap()
    {
        var map = pool.Get();
        return map;
    }

    //오브젝트 풀에 생성된 맵이 없을 경우, 새로운 맵 프리팹을 랜덤으로 생성해 풀에 제공
    private Map CreatingMap()
    {
        // 랜덤한 맵 생성
        Map map = Instantiate(mapPrefab[Random.Range(0, mapPrefab.Length)]).GetComponent<Map>();
        map.SetManagedPool(pool);
        return map;
    }

    //오브젝트 풀에서 맵을 가져올 때, 해당 맵 오브젝트 활성화
    private void OnGetMap(Map map)
    {
        map.gameObject.SetActive(true);
        // 활성화가 필요한 오브젝트들 활성화
        map.Initialize();
        //비활성화된 오브젝트 중에서도 찾을 수 있도록 true로 수정
        map.GetComponentInChildren<MapTrigger>(true).gameObject.SetActive(true);
    }

    //사용이 끝난 맵을 오브젝트 풀에 반환할 때, 해당 맵 오브젝트를 비활성화
    private void OnReleaseMap(Map map)
    {
        map.gameObject.SetActive(false);
    }

    //오브젝트 풀이 가득차거나 소멸될 때, 풀 안의 맵 오브젝트를 파괴
    private void OnDestroyMap(Map map)
    {
        Destroy(map.gameObject);
    }
}
