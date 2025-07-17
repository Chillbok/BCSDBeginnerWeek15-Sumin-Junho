using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 속도
    [SerializeField]
    private float speed;

    // 필요한 컴포넌트
    [SerializeField]
    private Rigidbody playerRb;

    void Update()
    {
        Move();
    }

    // 움직임
    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 velocity = new Vector3(moveDirX, 0, moveDirZ).normalized * speed;

        playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
