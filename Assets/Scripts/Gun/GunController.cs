using UnityEngine;

public class GunController : MonoBehaviour
{
    // �Ѿ� ����
    [SerializeField]
    private int bulletCount; // ������ �ִ� �Ѿ� ����
    [SerializeField]
    private int maxBulletCount; // źâ�� ���� �� �ִ� �ִ� �Ѿ� ����
    public int currentBulletCount; // źâ�� �ִ� �Ѿ� ����

    // �ʿ��� ������Ʈ
    [SerializeField]
    private GameObject Bullet;

    // ����
    public void Attack()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Bullet, transform.position + Vector3.up * 0.5f, Quaternion.Euler(transform.forward));
        }

        currentBulletCount--;
    }

    // ������
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
