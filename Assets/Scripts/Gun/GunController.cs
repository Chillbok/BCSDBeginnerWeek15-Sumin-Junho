using UnityEngine;

public class GunController : MonoBehaviour
{
    // �Ѿ� ����
    public int bulletCount;

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

        bulletCount--;
    }
}
