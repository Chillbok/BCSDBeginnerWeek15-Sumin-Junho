using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 체력
    [SerializeField]
    private float hp;
    private float currentHp;
    // 공격 속도
    [SerializeField]
    private float attackSpeed;
    // 공격력
    [SerializeField]
    private float damage;
    // 감지 범위
    [SerializeField]
    private float radius;
    // 회전 속도
    [SerializeField]
    private float rotationSpeed;

    // 터렛이 회전할 부위 (Rotation y 값)
    [SerializeField]
    private GameObject turretHead;

    // 참조 변수
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
