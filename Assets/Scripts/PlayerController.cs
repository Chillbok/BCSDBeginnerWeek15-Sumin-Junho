using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 속도
    public float walkSpeed; // 걷기 속도
    public float runSpeed; // 달리기 속도
    public float applySpeed; // 적용 속도

    // 플레이어의 점프 강도
    public float jumpForce;

    //플레이어의 체력
    [SerializeField]
    float hp; // 최대 체력
    public float currentHp; // 현재 체력

    // 플레이어의 스테미나
    [SerializeField]
    float sp; // 최대 스테미나 (5일경우 5초동안 사용 가능)
    float currentSp; // 현재 스테미나

    // 스테미나 회복 쿨타임
    [SerializeField]
    float spCooldown;
    float currentSpCooldown;

    // 시야 관련 변수
    [SerializeField]
    float lookSensitivity; // 카메라 민감도
    [SerializeField]
    float cameraRotationLimit; // 카메라 상하 한계 각도
    float currentCameraRotation = 0; // 현재 카메라 상하 각도


    // 상태 변수
    bool isGround = true; // 땅에 닿았는지 여부
    bool isRun = false; // 달리고 있는지 여부
    bool isSpUsed = false; // 스테미나 사용 여부
    bool isDead = false; // 플레이어 죽음 여부

    // 필요한 컴포넌트
    [SerializeField]
    Rigidbody playerRb;
    [SerializeField]
    Collider playerCol;
    [SerializeField]
    Camera theCamera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

        CameraRotation();
        CharacterRotation();
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

        Vector3 velocity = (transform.right * moveDirX + transform.forward * moveDirZ).normalized * applySpeed;

        playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    // 스테미나 회복
    void SPRecover()
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

    // 상하 카메라 회전
    void CameraRotation()
    {
        float rotation = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        currentCameraRotation -= rotation;
        currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotation, 0, 0);
    }

    // 좌우 캐릭터 회전
    void CharacterRotation()
    {
        float rotation = Input.GetAxisRaw("Mouse X") * lookSensitivity;
        Vector3 characterRotation = new Vector3(0, rotation, 0);

        playerRb.MoveRotation(playerRb.rotation * Quaternion.Euler(characterRotation));
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
