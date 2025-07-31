using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour
{
    // 공격력
    [SerializeField]
    private float damage;

    // 참조 변수
    Rigidbody bulletRb;
    PlayerController player;

    // 오브젝트 풀링 변수
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
