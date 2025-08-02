using UnityEngine;

public class Sawblade : MonoBehaviour
{
    // ������
    [SerializeField]
    private float damage;

    // �ʿ��� ��ǥ ����
    Vector3 left = new Vector3(-0.25f, 0, 0);
    Vector3 right = new Vector3(1.75f, 0, 0);

    // ���� ����
    private bool movingRight = true;

    void Update()
    {
        MoveSawblade();
    }

    // ��� ������
    private void MoveSawblade()
    {
        Vector3 target = movingRight ? right : left;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, target) < 0.01f)
            movingRight = !movingRight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.DecreaseHP(damage);
        }
    }
}
