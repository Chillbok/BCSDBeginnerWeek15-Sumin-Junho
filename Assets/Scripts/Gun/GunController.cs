using UnityEngine;

public class GunController : MonoBehaviour
{
    // ÃÑ¾Ë °¹¼ö
    public int bulletCount;

    // ÇÊ¿äÇÑ ÄÄÆ÷³ÍÆ®
    [SerializeField]
    private GameObject Bullet;


    // °ø°Ý
    public void Attack()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Bullet, transform.position + Vector3.up * 0.5f, Quaternion.Euler(transform.forward));
        }

        bulletCount--;
    }
}
