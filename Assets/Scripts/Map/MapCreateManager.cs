using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Pool;


public class MapCreateManager : Singleton<MapCreateManager>
{
    // 오브젝트 풀링 변수
    private IObjectPool<Map> pool;

    // 맵 프리팹
    [Header("맵")]
    [SerializeField]
    private GameObject[] mapPrefab;

    // 첫 시작 시
    private void Start()
    {
        var firstMap = CreateMap();
        firstMap.transform.position = Vector3.zero;
    }

    // 씬이 로드된 후
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlayScene")
        {
            var firstMap = CreateMap();
            firstMap.transform.position = Vector3.zero;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 맵 생성
    public Map CreateMap()
    {
        var map = pool.Get();
        return map;
    }

    // 풀에서 맵 생성하는 함수
    private Map CreatingMap()
    {
        // 랜덤한 맵 생성
        Map map = Instantiate(mapPrefab[Random.Range(0, mapPrefab.Length)]).GetComponent<Map>();
        map.SetManagedPool(pool);
        return map;
    }

    // 풀에서 오브젝트를 빌리는 함수
    private void OnGetMap(Map map)
    {
        map.gameObject.SetActive(true);
    }

    // 풀에서 오브젝트를 돌려줄 함수
    private void OnReleaseMap(Map map)
    {
        map.gameObject.SetActive(false);
    }

    // 풀에서 오브젝트를 파괴하는 함수
    private void OnDestroyMap(Map map)
    {
        Destroy(map.gameObject);
    }
}
