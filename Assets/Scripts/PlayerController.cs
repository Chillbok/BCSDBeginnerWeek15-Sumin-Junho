using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //플레이어의 속도
    [SerializeField]
    private float walkSpeed; // 기본 걷기 속도
    [SerializeField]
    private float runSpeed; // 기본 달리기 속도
    private float appliedWalkSpeed; //적용된 걷기 속도
    private float appliedRunSpeed; //적용된 달리기 속도
    private float currentSpeed; //적용할 속도

    //플레이어의 점프 강도
    [SerializeField]
    private float jumpForce; //기본 점프 강도
    private float appliedJumpForce; //적용할 점프 강도

    //플레이어의 체력
    [SerializeField]
    float hp; //플레이어 최대 체력
    public float currentHp; //플레이어 현재 체력

    //플레이어 스태미나
    [SerializeField]
    float sp; //플레이어 최대 스태미나 (5일 경우 5초 동안 사용 가능)
    float currentSp; //현재 스태미나

    //스태미나 회복까지 걸리는 쿨타임
    [SerializeField]
    float spCooldown;
    float currentSpCooldown;

    //시야 관련 변수
    [SerializeField]
    float lookSensitivity; //카메라 민감도
    [SerializeField]
    float cameraRotationLimit; //카메라 상하 한계 각도
    float currentCameraRotation = 0; //현재 카메라 상하 각도

    //상태 변수
    bool isGround = true; //땅에 닿았는지 여부
    bool isRun = false; //달리고 있는지 여부
    bool isSpUsed = false; //스테미나 사용 여부
    bool isDead = false; //플레이어 죽음 여부

    //필요한 컴포넌트
    [SerializeField]
    Rigidbody playerRb;
    [SerializeField]
    Collider playerCol;
    [SerializeField]
    Camera theCamera;
    
    //버프 보관용 딕셔너리
    private Dictionary<BuffType, Coroutine> activeBuffs = new Dictionary<BuffType, Coroutine>();
    //UI 표시를 위해 버프 남은 시간을 저장할 딕셔너리(공개용)
    public Dictionary<BuffType, float> buffRemainingTimes = new Dictionary<BuffType, float>();

    // 참조 변수
    [SerializeField]
    GunController theGunController;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //플레이어 스탯 초기화
        currentSpeed = walkSpeed;
        appliedWalkSpeed = walkSpeed;
        appliedRunSpeed = runSpeed;
        appliedJumpForce = jumpForce;
        currentSp = sp;
        currentHp = hp;
    }

    void Update()
    {
        TryJump();
        TryRun();
        TryFire();
        TryReload();
        Move();
        SPRecover();
        CheckIsGround();
        isDead = CheckDead();

		CameraRotation();
		CharacterRotation();
	}

    //새로 버프 적용하는 메서드
    //목표: 새로운 버프 적용하고, 만약 이미 같은 버프 있으면 기존 것 중지시키고 새로 시작해 시간 갱신
    public void ApplyBuff(BuffType buffType, float buffDuration, float multiplier)
    {
        if (activeBuffs.ContainsKey(buffType)) //만약 같은 종류의버프가 이미 활성화되었다면
        {
            //기존 버프의 코루틴 멈추기
            StopCoroutine(activeBuffs[buffType]);
        }
        
        //새로운 버프 효과를 적용하고 지속시간을 관리할 코루틴 시작
        Coroutine buffCoroutine = StartCoroutine(BuffCoroutine(buffType, buffDuration, multiplier));
        //딕셔너리에 새로운 코루틴 저장(또는 갱신)
        activeBuffs[buffType] = buffCoroutine;
    }

    private IEnumerator BuffCoroutine(BuffType buffType, float buffDuration, float multiplier)
    {
        //버프 효과 적용
        Debug.Log($"{buffType} 버프 적용 시작! 지속 시간: {buffDuration}초");

        switch (buffType)
        {
            /*
            *=는 사용하지않는다.
            *= multiplier로 표현할 시 이전 효과에 중첩되는 문제가 발생한다.
            */
            case (BuffType.AddSpeed): //속도 증가 버프
                appliedRunSpeed = runSpeed * multiplier;
                appliedWalkSpeed = walkSpeed * multiplier;
                break;
            case (BuffType.SuperJump): //점프 높이 증가 버프
                appliedJumpForce = jumpForce * multiplier;
                break;
            case (BuffType.HealthRegen): //체력 회복 버프
                break;
        }

        //남은 시간을 딕셔너리에 추가
        buffRemainingTimes[buffType] = buffDuration;

        //지정한 지속시간만큼 반복해서 대기
        while (buffRemainingTimes[buffType] > 0)
        {
            buffRemainingTimes[buffType] -= Time.deltaTime; //1프레임만큼 시간 차감
            yield return null; //다음 프레임까지 대기
        }

        //버프 효과 제거
        Debug.Log($"{buffType} 버프 종료!");
        switch (buffType)
        {
            case (BuffType.AddSpeed): //속도 증가 버프 제거
                appliedRunSpeed = runSpeed;
                appliedWalkSpeed = walkSpeed;
                break;
            case (BuffType.SuperJump): //슈퍼 점프 버프 제거
                appliedJumpForce = jumpForce;
                break;
            case (BuffType.HealthRegen): //체력 회복 버프 제거
                break;
        }

        //딕셔너리에서 해당 버프 정보 제거
        activeBuffs.Remove(buffType);
        buffRemainingTimes.Remove(buffType); //남은 시간 딕셔너리에서 제거
    }

    //점프 시도
    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            Jump();
    }

    //점프
    void Jump()
    {
        playerRb.linearVelocity = transform.up * appliedJumpForce;
    }

    //달리기 시도
    void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentSp > 0)
            Run();
        else if (Input.GetKeyUp(KeyCode.LeftShift) || currentSp <= 0)
            RunCancel();
    }

    //달리기
    void Run()
    {
        isRun = true;
        currentSp -= Time.deltaTime;

        currentSpeed = appliedRunSpeed;
    }

    //달리기 취소
    void RunCancel()
    {
        isRun = false;
        currentSpeed = appliedWalkSpeed;
    }

    // 발사 시도
    void TryFire()
    {
        if (Input.GetMouseButtonDown(0) && !GunController.isReload)
            theGunController.Fire();
    }

    // 재장전 시도
    void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !GunController.isReload)
            StartCoroutine(theGunController.Reload());
    }

    // 움직임
    void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 velocity = (transform.right * moveDirX + transform.forward * moveDirZ).normalized * currentSpeed;

		playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
	}

    //스태미나 회복
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

    //플레이어가 땅에 닿아있는지에 대한 여부 판별
    void CheckIsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCol.bounds.extents.y + 0.1f);
    }

    //플레이어의 사망 여부 출력하는 메서드
    //hp가 0이 되면 true 반환
    bool CheckDead()
    {
        if (hp > 0) //hp가 0 이상이면 (안 죽었으면)
            return false;
        return true;
    }

    //상하 카메라 회전
    void CameraRotation()
    {
        float rotation = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        currentCameraRotation -= rotation;
        currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraRotationLimit, cameraRotationLimit);

		theCamera.transform.localEulerAngles = new Vector3(currentCameraRotation, 0, 0);
	}

    //좌우 캐릭터 회전
    void CharacterRotation()
    {
        float rotation = Input.GetAxisRaw("Mouse X") * lookSensitivity;
        Vector3 characterRotation = new Vector3(0, rotation, 0);

		playerRb.MoveRotation(playerRb.rotation * Quaternion.Euler(characterRotation));
	}

    //플레이어 정보 내보내는 메서드들
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
