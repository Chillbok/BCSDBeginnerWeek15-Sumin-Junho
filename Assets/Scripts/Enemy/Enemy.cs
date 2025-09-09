using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{

    //포탑 데이터 들어있는 스크립터블 오브젝트
    [Header("포탑 데이터 Scriptable Object")]
    [SerializeField]
    private TurretSO turretSO;
    
    [Header("참조 변수")]
    // 참조 변수
    [SerializeField]
    private GameObject bulletPrefab;
    private GunController gun;
    [SerializeField]
    private LayerMask layer;

    //현재 체력
    private float currentHp;

    // 공격 방향
    Vector3 attackDirection;

    [Header("터렛 부품 변수들")]
    // 터렛이 회전할 부위 (Rotation y 값)
    [SerializeField]
    private GameObject turretHead;

    // 총구 부위
    [SerializeField]
    private Transform muzzle;

    // 중복 발사 방지용 코루틴 변수
    private Coroutine fire_coroutine;

    // 상태 변수
    private bool isPlayerDetected;


    // 오브젝트 풀링 변수
    private IObjectPool<EnemyBullet> pool;

    private void Awake()
    {
        attackDirection = Vector3.zero;
        gun = FindObjectOfType<GunController>();
        pool = new ObjectPool<EnemyBullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, maxSize: 2);
    }

    void Start()
    {
        currentHp = turretSO.HP;
    }

    void Update()
    {
        DetectPlayer();
        TryFire();

        if (CheckDead())
        {
            gun.leftBulletCount += 10;
            Destroy(gameObject);
        }
    }

    // 플레이어 감지
    private void DetectPlayer()
    {
        isPlayerDetected = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, turretSO.AttackRange);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                isPlayerDetected = true;
                LookPlayer(col);
                break;
            }
        }
    }

    // 플레이어 바라보기
    private void LookPlayer(Collider target)
    {
        attackDirection = (target.transform.position - turretHead.transform.position).normalized;

        turretHead.transform.rotation = Quaternion.Lerp(turretHead.transform.rotation, Quaternion.LookRotation(attackDirection), turretSO.TowerRotationSpeed * Time.deltaTime);
    }

    // 발사 시도
    private void TryFire()
    {
        if (isPlayerDetected)
        {
            if (fire_coroutine == null)
            {
                fire_coroutine = StartCoroutine(Fire());
            }
        }
        else
        {
            if (fire_coroutine != null)
            {
                StopCoroutine(fire_coroutine);
                fire_coroutine = null;
            }
        }
    }

    // 발사
    private IEnumerator Fire()
    {
        while (true)
        {
            var bullet = pool.Get();
            bullet.transform.position = muzzle.position;
            bullet.GetComponent<Rigidbody>().AddForce(attackDirection * turretSO.BulletSpeed, ForceMode.Impulse);

            yield return new WaitForSeconds(turretSO.AttackSpeed);
        }
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
