using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 속도
    [SerializeField]
    private float walkSpeed; // 걷기 속도
    [SerializeField]
    private float runSpeed; // 달리기 속도
    private float applySpeed; // 적용 속도

    // 플레이어의 점프 강도
    [SerializeField]
    private float jumpForce;

    //플레이어의 체력
    [SerializeField]
    private float hp; // 최대 체력
    private float currentHp; // 현재 체력

    // 플레이어의 스테미나
    [SerializeField]
    private float sp; // 최대 스테미나 (5일경우 5초동안 사용 가능)
    private float currentSp; // 현재 스테미나

    // 스테미나 회복 쿨타임
    [SerializeField]
    private float spCooldown;
    private float currentSpCooldown;

    // 상태 변수
    private bool isGround = true; // 땅에 닿았는지 여부
    private bool isRun = false; // 달리고 있는지 여부
    private bool isSpUsed = false; // 스테미나 사용 여부
    private bool isDead = false; // 플레이어 죽음 여부

    // 필요한 컴포넌트
    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private Collider playerCol;

    private void Start()
    {
        applySpeed = walkSpeed;
        currentSp = sp;
        currentHp = hp;
    }

    void Update()
    {
        TryJump();
        TryRun();
        Move();
        SPRecover();
        CheckIsGround();
        isDead = CheckDead();
    }

    // 점프 시도
    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            Jump();
    }

    // 점프
    void Jump()
    {
        playerRb.linearVelocity = transform.up * jumpForce;
    }

    // 달리기 시도
    void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentSp > 0)
            Run();
        else if (Input.GetKeyUp(KeyCode.LeftShift) || currentSp <= 0)
            RunCancel();
    }

    // 달리기
    void Run()
    {
        isRun = true;
        currentSp -= Time.deltaTime;

        applySpeed = runSpeed;
    }

    // 달리기 취소
    void RunCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    // 움직임
    void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 velocity = new Vector3(moveDirX, 0, moveDirZ).normalized * applySpeed;

        playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    // 스테미나 회복
    void SPRecover()
    {
        if (!isRun) //
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
    void CheckIsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCol.bounds.extents.y + 0.1f);
    }

    // 플레이어의 사망 여부 출력하는 메서드
    // hp가 0이 되면 true 반환
    bool CheckDead()
    {
        if (hp > 0) // hp가 0 이상이면 (안 죽었으면)
            return false;
        return true;
    }

    // 플레이어 정보 내보내는 메서드들
    #region GetMethods
    public float GetPlayerCurrentHP()
    {
        return currentHp;
    }

    public float GetPlayerHP()
    {
        return hp;
    }

    public float GetPlayerCurrentSP()
    {
        return currentSp;
    }

    public float GetPlayerSP()
    {
        return sp;
    }
    #endregion GetMethods
}
