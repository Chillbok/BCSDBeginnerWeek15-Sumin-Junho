using UnityEngine;
using UnityEngine.Pool;

public class Map : MonoBehaviour
{
    // 오브젝트 풀링 변수
    private IObjectPool<Map> managedPool;

    // 재활성화 필요 오브젝트
    [SerializeField]
    private GameObject[] needReactiveObjects;

    // 재활성화 함수
    public void Initialize()
    {
        foreach (var obj in needReactiveObjects)
        {
            if (!obj.activeSelf)
                obj.SetActive(true);
        }
    }

    // 오브젝트 풀링 함수
    public void SetManagedPool(IObjectPool<Map> pool)
    {
        managedPool = pool;
    }

    // 맵 파괴
    public void DestroyMap()
    {
        managedPool.Release(this);
    }
}
