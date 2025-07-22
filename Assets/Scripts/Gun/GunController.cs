using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // �Ѿ� ����
    [SerializeField]
    private int bulletCount; // ������ �ִ� �Ѿ� ����
    [SerializeField]
    private int maxBulletCount; // źâ�� ���� �� �ִ� �ִ� �Ѿ� ����
    public int currentBulletCount; // źâ�� �ִ� �Ѿ� ����

    // ���� ����
    public bool isReload = false; // ������ ����

    // �ʿ��� ������Ʈ
    [SerializeField]
    private GameObject Bullet;

    // �߻� �غ�
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

    // �߻�
    private void Shoot()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Bullet, transform.position + Vector3.up * 0.5f, Quaternion.Euler(transform.forward));
        }

        currentBulletCount--;
    }

    // ������
     public IEnumerator Reload()
    {
        if (bulletCount > 0)
        {
            isReload = true;

            int needBulletCount = maxBulletCount - currentBulletCount;

            yield return new WaitForSeconds(1.5f);

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

            isReload = false;
        }
    }
}
