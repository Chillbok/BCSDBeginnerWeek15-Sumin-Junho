using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    // ü��
    [SerializeField]
    private float hp;
    private float currentHp;
    // ���� �ӵ�
    [SerializeField]
    private float attackSpeed;
    // ���� ����
    [SerializeField]
    private float radius;
    // ȸ�� �ӵ�
    [SerializeField]
    private float rotationSpeed;
    //�Ѿ� �ӵ�
    [SerializeField]
    float speed;

    // ���� ����
    Vector3 attackDirection;

    // ���� ����
    private bool isFire = false;

    // �ͷ��� ȸ���� ���� (Rotation y ��)
    [SerializeField]
    private GameObject turretHead;

    // �ѱ� ����
    [SerializeField]
    private Transform muzzle;

    // ���� ����
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GunController gun;

    // ������Ʈ Ǯ�� ����
    private IObjectPool<EnemyBullet> pool;

    private void Awake()
    {
        attackDirection = Vector3.zero;
        pool = new ObjectPool<EnemyBullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, maxSize: 1);
    }

    void Start()
    {
        currentHp = hp;
    }

    void Update()
    {
        DetectPlayer();

        if (CheckDead())
        {
            gun.leftBulletCount += 10;
            Destroy(gameObject);
        }
    }

    // �÷��̾� ����
    private void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                LookPlayer(col);
                TryFire();
            }
        }
    }

    // �÷��̾� �ٶ󺸱�
    private void LookPlayer(Collider target)
    {
        attackDirection = (target.transform.position - turretHead.transform.position).normalized;

        turretHead.transform.rotation = Quaternion.Lerp(turretHead.transform.rotation, Quaternion.LookRotation(attackDirection), rotationSpeed * Time.deltaTime);
    }

    // �߻� �õ�
    private void TryFire()
    {
        Debug.DrawRay(muzzle.position, muzzle.right * 5, Color.red);

        // ���̾� 3�� - Player
        if (Physics.Raycast(muzzle.position, muzzle.right, radius, 3))
        {
            if (!isFire)
            {
                InvokeRepeating("Fire", 0, attackSpeed);
                isFire = true;
            }
        }
        else
        {
            CancelInvoke("Fire");
            isFire = false;
        }
    }

    // �߻�
    private void Fire()
    {
        var bullet = pool.Get();
        bullet.transform.position = muzzle.position;
        bullet.GetComponent<Rigidbody>().AddForce(attackDirection * speed, ForceMode.Impulse);
    }

    private bool CheckDead()
    {
        if (currentHp > 0)
            return false;
        else
            return true;
    }

    // ���� ���� ��������
    public Vector3 GetAttackDirection()
    {
        return attackDirection;
    }

    // ü�� ����
    public void DecreaseHP(float damage)
    {
        currentHp -= damage;
    }

    // �Ѿ� ����
    private EnemyBullet CreateBullet()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab).GetComponent<EnemyBullet>();
        bullet.SetManagedPool(pool);
        return bullet;
    }

    // Ǯ���� ������Ʈ�� ������ �Լ�
    private void OnGetBullet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    // Ǯ���� ������Ʈ�� ������ �Լ�
    private void OnReleaseBullet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    // Ǯ���� ������Ʈ�� �ı��ϴ� �Լ�
    private void OnDestroyBullet(EnemyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
