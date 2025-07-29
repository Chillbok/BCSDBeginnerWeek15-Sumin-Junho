using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    //총알 사거리
    [SerializeField]
    float range;
    //총알 분산도
    [SerializeField]
    float spread;

    // 참조 변수
    Rigidbody bulletRb;
    [SerializeField]
    GameObject theCamera;

    // 오브젝트 풀링 변수
    private IObjectPool<Bullet> managedPool;

    void Awake()
    {
        bulletRb = GetComponent<Rigidbody>();
        theCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void OnEnable()
    {
        Vector3 shotSpread = Random.insideUnitSphere * spread;
        bulletRb.AddForce((theCamera.transform.forward * range) + shotSpread, ForceMode.Impulse);
        Invoke("DestroyBullet", 1f);
    }

    void OnDisable()
    {
        CancelInvoke();
        bulletRb.linearVelocity = Vector3.zero;
        bulletRb.angularVelocity = Vector3.zero;
    }

    // 오브젝트 풀링 함수
    public void SetManagedPool(IObjectPool<Bullet> pool)
    {
        managedPool = pool;
    }

    // 총알 파괴
    public void DestroyBullet()
    {
        managedPool.Release(this);
    }
}
