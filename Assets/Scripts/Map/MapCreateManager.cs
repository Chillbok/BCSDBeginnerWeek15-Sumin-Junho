using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Pool;


public class MapCreateManager : Singleton<MapCreateManager>
{
    // ������Ʈ Ǯ�� ����
    private IObjectPool<Map> pool;

    [Header("스크립터블 오브젝트")]
    [Tooltip("게임 툴팁")]
    [SerializeField]
    private GameManagerSO gameManagerSO;

    // �� ������
    [Header("��")]
    [SerializeField]
    private GameObject[] mapPrefab;

    // ���� �ε�� ��
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

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

    // �� ����
    public Map CreateMap()
    {
        var map = pool.Get();
        return map;
    }

    // Ǯ���� �� �����ϴ� �Լ�
    private Map CreatingMap()
    {
        // ������ �� ����
        Map map = Instantiate(mapPrefab[Random.Range(0, mapPrefab.Length)]).GetComponent<Map>();
        map.SetManagedPool(pool);
        return map;
    }

    // Ǯ���� ������Ʈ�� ������ �Լ�
    private void OnGetMap(Map map)
    {
        map.gameObject.SetActive(true);
        map.GetComponentInChildren<MapTrigger>().gameObject.SetActive(true);
    }

    // Ǯ���� ������Ʈ�� ������ �Լ�
    private void OnReleaseMap(Map map)
    {
        map.gameObject.SetActive(false);
    }

    // Ǯ���� ������Ʈ�� �ı��ϴ� �Լ�
    private void OnDestroyMap(Map map)
    {
        Destroy(map.gameObject);
    }
}
