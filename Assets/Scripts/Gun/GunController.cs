using UnityEngine;

public class GunController : MonoBehaviour
{
    // �ʿ��� ������Ʈ
    [SerializeField]
    GameObject Bullet;


    // ����
    public void Attack()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Bullet, transform.position + Vector3.up * 0.5f, Quaternion.Euler(transform.forward));
        }
    }
}
