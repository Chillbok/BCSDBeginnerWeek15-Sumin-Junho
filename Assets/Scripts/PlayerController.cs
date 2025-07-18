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

    //플레이어의 체력
    [SerializeField]
    private int hp;

    // 플레이어의 스테미나
    [SerializeField]
    private float sp;
    private float currentSp;

    // 스테미나 회복 쿨타임
    [SerializeField]
    private float spCooldown;
    private float currentSpCooldown;

    // 상태 변수
    private bool isGround = true;
    private bool isRun = false;
    private bool isSpUsed = false;

    // 필요한 컴포넌트
    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private Collider playerCol;

    private void Start()
    {
        applySpeed = walkSpeed;
        currentSp = sp;
    }

    void Update()
    {
        TryJump();
        TryRun();
        Move();
        SPRecover();
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
        if (Input.GetKey(KeyCode.LeftShift) && currentSp > 0)
            Run();
        else if (Input.GetKeyUp(KeyCode.LeftShift) || currentSp <= 0)
            RunCancel();
    }

    // 달리기
    private void Run()
    {
        isRun = true;
        currentSp -= Time.deltaTime;

        applySpeed = runSpeed;
    }

    // 달리기 취소
    private void RunCancel()
    {
        isRun = false;
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

    // 스테미나 회복
    private void SPRecover()
    {
        if (!isRun)
        {
            if (isSpUsed)
            {
                if (currentSpCooldown < spCooldown)
                {
                    currentSpCooldown += Time.deltaTime;
                }
                else
                {
                    isSpUsed = false;
                    currentSpCooldown = 0;
                }
            }
            else if (!isSpUsed)
            {
                if (currentSp < sp)
                    currentSp += Time.deltaTime;
                else
                    currentSp = sp;
            }
        }
        else
        {
            isSpUsed = true;
            currentSpCooldown = 0;
        }
    }

    // 플레이어가 땅에 닿아있는지에 대한 여부 판별
    private void CheckIsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCol.bounds.extents.y + 0.1f);
    }
}
