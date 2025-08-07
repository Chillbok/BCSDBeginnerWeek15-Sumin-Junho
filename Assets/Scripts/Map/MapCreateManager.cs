using UnityEngine;
using UnityEngine.Pool;

public class MapCreateManager : MonoBehaviour
{
    // 오브젝트 풀링 변수
    private IObjectPool<Map> pool;

    // 맵 프리팹
    [Header("맵")]
    [SerializeField]
    private GameObject[] mapPrefab;

    void Awake()
    {
        pool = new ObjectPool<Map>(CreateMap, OnGetMap, OnReleaseMap, OnDestroyMap, maxSize: 2);
    }

    // 맵 생성
    private Map CreateMap()
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
