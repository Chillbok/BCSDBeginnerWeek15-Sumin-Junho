using UnityEngine;

public class Sawblade : MonoBehaviour
{
    // ������
    [SerializeField]
    private float damage;

    // ������ �ӵ�
    [SerializeField]
    private float speed;

    // �ʿ��� ��ǥ ����
    Vector3 left = new Vector3(-0.25f, 0, 0);
    Vector3 right = new Vector3(1.75f, 0, 0);

    void Update()
    {
        MoveSawblade();
    }

    // ��� ������
    private void MoveSawblade()
    {
        float time = Mathf.PingPong(Time.time * speed, 1.0f);

        transform.localPosition = Vector3.Lerp(left, right, time);
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
