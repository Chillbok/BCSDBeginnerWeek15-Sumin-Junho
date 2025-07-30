using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour
{
    // ���� ����
    Rigidbody bulletRb;

    // ������Ʈ Ǯ�� ����
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

    // ������Ʈ Ǯ�� �Լ�
    public void SetManagedPool(IObjectPool<EnemyBullet> pool)
    {
        managedPool = pool;
    }

    // �Ѿ� �ı�
    private void DestroyBullet()
    {
        managedPool.Release(this);
    }
}
