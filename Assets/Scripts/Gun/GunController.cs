using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // 상태 변수
    public static bool isReload = false; // 재장전 여부

    // 필요한 컴포넌트
    [SerializeField]
    private Gun gun;
    [SerializeField]
    private GameObject Bullet;

    // 발사 준비
    public void Fire()
    {
        if (!isReload)
        {
            if (gun.currentBulletCount > 0)
                Shoot();
            else
                StartCoroutine(Reload());
        }
    }

    // 발사
    private void Shoot()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Bullet, transform.position + Vector3.up * 0.5f, Quaternion.Euler(transform.forward));
        }

        gun.currentBulletCount--;
    }

    // 재장전
    public IEnumerator Reload()
    {
        if (gun.leftBulletCount > 0 && gun.currentBulletCount != gun.maxBulletCount) //총알 개수가 0개보다 크고, 현재 탄창의 총알 개수가 탄창 최대 총알 개수와 같지 않을 때
        {
            isReload = true; //재장전 활성화

            int needBulletCount = gun.maxBulletCount - gun.currentBulletCount; //탄창에 더 들어가야 할 총알

            yield return new WaitForSeconds(1.5f);

            if (gun.leftBulletCount >= needBulletCount) //총 총알 개수가 탄창에 더 넣어야 하는 총알만큼 있다면
            {
                gun.currentBulletCount = gun.maxBulletCount; //탄창 최대 용량으로 총알 넣기
                gun.leftBulletCount -= needBulletCount; //총 총알 개수에서 탄창에 더 넣은 총알만큼 빼기
            }
            else //총 총알 개수가 탄창에 더 넣어야 할 총알 개수보다 적다면
            {
                gun.currentBulletCount += gun.leftBulletCount;
                gun.leftBulletCount = 0;
            }

            isReload = false; //재장전 비활성화(재장전 끝)
        }
    }
}
