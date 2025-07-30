using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour
{
    // 참조 변수
    Rigidbody bulletRb;

    // 오브젝트 풀링 변수
    private IObjectPool<EnemyBullet> managedPool;

    void Awake()
    {
        bulletRb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        Invoke("DestroyBullet", 1f);
    }

    void OnDisable()
    {
        CancelInvoke();
        bulletRb.linearVelocity = Vector3.zero;
        bulletRb.angularVelocity = Vector3.zero;
    }

    // 오브젝트 풀링 함수
    public void SetManagedPool(IObjectPool<EnemyBullet> pool)
    {
        managedPool = pool;
    }

    // 총알 파괴
    private void DestroyBullet()
    {
        managedPool.Release(this);
    }
}
