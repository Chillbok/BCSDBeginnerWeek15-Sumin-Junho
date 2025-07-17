using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �÷��̾��� �ӵ�
    [SerializeField]
    private float speed;
    // �÷��̾��� ���� ����
    [SerializeField]
    private float jumpForce;

    // ���� ����
    private bool isGround = true;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private Collider playerCol;

    void Update()
    {
        Move();
        CheckIsGround();
        TryJump();
    }

    // ������
    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 velocity = new Vector3(moveDirX, 0, moveDirZ).normalized * speed;

        playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    // �÷��̾ ���� ����ִ����� ���� ���� �Ǻ�
    private void CheckIsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCol.bounds.extents.y + 0.1f);
    }

    // ���� �õ�
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            Jump();
    }

    // ����
    private void Jump()
    {
        playerRb.linearVelocity = transform.up * jumpForce;
    }
}
