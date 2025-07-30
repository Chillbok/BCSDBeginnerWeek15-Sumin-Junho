using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class GunController : MonoBehaviour
{
    // 총 관련 변수들
    public int leftBulletCount; // 남은 총알 개수
    public int maxBulletCount; // 최대 탄창 총알 개수
    public int currentBulletCount; // 현재 탄창 총알 개수

    // 상태 변수
    public static bool isReload = false; // 재장전 여부

    // 필요한 컴포넌트
    [SerializeField]
    private Transform muzzle; // 총구 위치
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Animator gunAnim;

    // 오브젝트 풀링 변수
    private IObjectPool<Bullet> pool;

    private void Awake()
    {
        pool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, maxSize:16);
    }

    // 발사 준비
    public void Fire()
    {
        if (!isReload)
        {
            if (currentBulletCount > 0)
                Shoot();
            else
                StartCoroutine(Reload());
        }
    }

    // 발사
    private void Shoot()
    {
        gunAnim.SetTrigger("Attack");
        SoundManager.instance.PlaySFX("shotgun_fire");

        for (int i = 0; i < 8; i++)
        {
            var bullet = pool.Get();
            bullet.transform.position = muzzle.position;
        }

        currentBulletCount--;
    }

    // 재장전
    public IEnumerator Reload()
    {

        if (leftBulletCount > 0 && currentBulletCount != maxBulletCount) //총알 개수가 0개보다 크고, 현재 탄창의 총알 개수가 탄창 최대 총알 개수와 같지 않을 때
        {
            gunAnim.SetTrigger("Reload");
            SoundManager.instance.PlaySFX("shotgun_reload");

            isReload = true; //재장전 활성화

            int needBulletCount = maxBulletCount - currentBulletCount; //탄창에 더 들어가야 할 총알

            yield return new WaitForSeconds(2f);

            if (leftBulletCount >= needBulletCount) //총 총알 개수가 탄창에 더 넣어야 하는 총알만큼 있다면
            {
                currentBulletCount = maxBulletCount; //탄창 최대 용량으로 총알 넣기
                leftBulletCount -= needBulletCount; //총 총알 개수에서 탄창에 더 넣은 총알만큼 빼기
            }
            else //총 총알 개수가 탄창에 더 넣어야 할 총알 개수보다 적다면
            {
                currentBulletCount += leftBulletCount;
                leftBulletCount = 0;
            }

            isReload = false; //재장전 비활성화(재장전 끝)
        }
    }

    // 총알 생성
    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.SetManagedPool(pool);
        return bullet;
    }

    // 풀에서 오브젝트를 빌리는 함수
    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    // 풀에서 오브젝트를 돌려줄 함수
    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    // 풀에서 오브젝트를 파괴하는 함수
    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
