using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 속도
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applySpeed;

    // 플레이어의 점프 강도
    [SerializeField]
    private float jumpForce;

    // 상태 변수
    private bool isGround = true;

    // 필요한 컴포넌트
    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private Collider playerCol;

    private void Start()
    {
        applySpeed = walkSpeed;
    }

    void Update()
    {
        TryJump();
        TryRun();
        Move();
        CheckIsGround();
    }

    // 점프 시도
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            Jump();
    }

    // 점프
    private void Jump()
    {
        playerRb.linearVelocity = transform.up * jumpForce;
    }

    // 달리기 시도
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            Run();
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            RunCancel();
    }

    // 달리기
    private void Run()
    {
        applySpeed = runSpeed;
    }

    // 달리기 취소
    private void RunCancel()
    {
        applySpeed = walkSpeed;
    }

    // 움직임
    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 velocity = new Vector3(moveDirX, 0, moveDirZ).normalized * applySpeed;

        playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    // 플레이어가 땅에 닿아있는지에 대한 여부 판별
    private void CheckIsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCol.bounds.extents.y + 0.1f);
    }
}
