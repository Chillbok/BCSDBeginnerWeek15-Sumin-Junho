using UnityEngine;

public class GunController : MonoBehaviour
{
    // 총알 갯수
    [SerializeField]
    private int bulletCount; // 가지고 있는 총알 갯수
    [SerializeField]
    private int maxBulletCount; // 탄창에 넣을 수 있는 최대 총알 갯수
    public int currentBulletCount; // 탄창에 있는 총알 갯수

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject Bullet;

    // 공격
    public void Attack()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Bullet, transform.position + Vector3.up * 0.5f, Quaternion.Euler(transform.forward));
        }

        currentBulletCount--;
    }

    // 재장전
    public void Reload()
    {
        int needBulletCount = maxBulletCount - currentBulletCount;

        if (bulletCount >= needBulletCount)
        {
            currentBulletCount = maxBulletCount;
            bulletCount -= needBulletCount;
        }
        else
        {
            currentBulletCount += bulletCount;
            bulletCount = 0;
        }
    }
}
