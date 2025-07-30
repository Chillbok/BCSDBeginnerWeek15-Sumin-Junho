using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ü��
    [SerializeField]
    private float hp;
    private float currentHp;
    // ���� �ӵ�
    [SerializeField]
    private float attackSpeed;
    // ���ݷ�
    [SerializeField]
    private float damage;
    // ���� ����
    [SerializeField]
    private float radius;
    // ȸ�� �ӵ�
    [SerializeField]
    private float rotationSpeed;

    // �ͷ��� ȸ���� ���� (Rotation y ��)
    [SerializeField]
    private GameObject turretHead;

    // ���� ����
    [SerializeField]
    private GameObject bulletPrefab;

    void Start()
    {
        currentHp = hp;
    }

    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                LookPlayer(col);
            }
        }
    }

    private void LookPlayer(Collider target)
    {
        Vector3 attackDirection = (target.transform.position - turretHead.transform.position).normalized;

        turretHead.transform.rotation = Quaternion.Lerp(turretHead.transform.rotation, Quaternion.LookRotation(attackDirection), rotationSpeed * Time.deltaTime);
    }
}
