using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // ���� ����
    public static bool isReload = false; // ������ ����

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Gun gun;
    [SerializeField]
    private GameObject Bullet;

    // �߻� �غ�
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

    // �߻�
    private void Shoot()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Bullet, transform.position + Vector3.up * 0.5f, Quaternion.Euler(transform.forward));
        }

        gun.currentBulletCount--;
    }

    // ������
     public IEnumerator Reload()
    {
        if (gun.bulletCount > 0)
        {
            isReload = true;

            int needBulletCount = gun.maxBulletCount - gun.currentBulletCount;

            yield return new WaitForSeconds(1.5f);

            if (gun.bulletCount >= needBulletCount)
            {
                gun.currentBulletCount = gun.maxBulletCount;
                gun.bulletCount -= needBulletCount;
            }
            else
            {
                gun.currentBulletCount += gun.bulletCount;
                gun.bulletCount = 0;
            }

            isReload = false;
        }
    }
}
