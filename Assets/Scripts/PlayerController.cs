using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �÷��̾��� �ӵ�
    [SerializeField]
    private float speed;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Rigidbody playerRb;

    void Update()
    {
        Move();
    }

    // ������
    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 velocity = new Vector3(moveDirX, 0, moveDirZ).normalized * speed;

        playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
