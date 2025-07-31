using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour
{
    // ���ݷ�
    [SerializeField]
    private float damage;

    // ���� ����
    Rigidbody bulletRb;
    PlayerController player;

    // ������Ʈ Ǯ�� ����
    private IObjectPool<EnemyBullet> managedPool;

    void Awake()
    {
        bulletRb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            player.DecreaseHP(damage);
        else
            DestroyBullet();
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
