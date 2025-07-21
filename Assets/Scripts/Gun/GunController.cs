using UnityEngine;

public class GunController : MonoBehaviour
{
    // 필요한 컴포넌트
    [SerializeField]
    GameObject Bullet;


    // 공격
    public void Attack()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Bullet, transform.position + Vector3.up * 0.5f, Quaternion.Euler(transform.forward));
        }
    }
}
