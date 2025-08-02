using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    // 체력
    [SerializeField]
    private float hp;
    private float currentHp;
    // 공격 속도
    [SerializeField]
    private float attackSpeed;
    // 감지 범위
    [SerializeField]
    private float radius;
    // 회전 속도
    [SerializeField]
    private float rotationSpeed;
    //총알 속도
    [SerializeField]
    float speed;

    // 공격 방향
    Vector3 attackDirection;

    // 상태 변수
    private bool isFire = false;

    // 터렛이 회전할 부위 (Rotation y 값)
    [SerializeField]
    private GameObject turretHead;

    // 총구 부위
    [SerializeField]
    private Transform muzzle;

    // 참조 변수
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GunController gun;

    // 오브젝트 풀링 변수
    private IObjectPool<EnemyBullet> pool;

    private void Awake()
    {
        attackDirection = Vector3.zero;
        pool = new ObjectPool<EnemyBullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, maxSize: 1);
    }

    void Start()
    {
        currentHp = hp;
    }

    void Update()
    {
        DetectPlayer();

        if (CheckDead())
        {
            gun.leftBulletCount += 10;
            Destroy(gameObject);
        }
    }

    // 플레이어 감지
    private void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                LookPlayer(col);
                TryFire();
            }
        }
    }

    // 플레이어 바라보기
    private void LookPlayer(Collider target)
    {
        attackDirection = (target.transform.position - turretHead.transform.position).normalized;

        turretHead.transform.rotation = Quaternion.Lerp(turretHead.transform.rotation, Quaternion.LookRotation(attackDirection), rotationSpeed * Time.deltaTime);
    }

    // 발사 시도
    private void TryFire()
    {
        Debug.DrawRay(muzzle.position, muzzle.right * 5, Color.red);

        // 레이어 3번 - Player
        if (Physics.Raycast(muzzle.position, muzzle.right, radius, 3))
        {
            if (!isFire)
            {
                InvokeRepeating("Fire", 0, attackSpeed);
                isFire = true;
            }
        }
        else
        {
            CancelInvoke("Fire");
            isFire = false;
        }
    }

    // 발사
    private void Fire()
    {
        var bullet = pool.Get();
        bullet.transform.position = muzzle.position;
        bullet.GetComponent<Rigidbody>().AddForce(attackDirection * speed, ForceMode.Impulse);
    }

    private bool CheckDead()
    {
        if (currentHp > 0)
            return false;
        else
            return true;
    }

    // 공격 방향 가져오기
    public Vector3 GetAttackDirection()
    {
        return attackDirection;
    }

    // 체력 감소
    public void DecreaseHP(float damage)
    {
        currentHp -= damage;
    }

    // 총알 생성
    private EnemyBullet CreateBullet()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab).GetComponent<EnemyBullet>();
        bullet.SetManagedPool(pool);
        return bullet;
    }

    // 풀에서 오브젝트를 빌리는 함수
    private void OnGetBullet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    // 풀에서 오브젝트를 돌려줄 함수
    private void OnReleaseBullet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    // 풀에서 오브젝트를 파괴하는 함수
    private void OnDestroyBullet(EnemyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
