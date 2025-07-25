using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �댁댁 
    [SerializeField]
    private float walkSpeed; // 湲곕낯 嫄룰린 
    [SerializeField]
    private float runSpeed; // 湲곕낯 щ━湲 
    private float appliedWalkSpeed; //�⑸ 嫄룰린 
    private float appliedRunSpeed; //�⑸ щ━湲 
    [SerializeField]
    private float currentSpeed; //�⑺ 

    // �댁댁 � 媛
    [SerializeField]
    private float jumpForce; //湲곕낯 � 媛
    private float appliedJumpForce; //�⑺ � 媛

    //�댁댁 泥대
    [SerializeField]
    float hp; // 理 泥대
    public float currentHp; //  泥대

    // �댁댁 ㅽ誘몃
    [SerializeField]
    float sp; // 理 ㅽ誘몃 (5쇨꼍 5珥 ъ 媛)
    float currentSp; //  ㅽ誘몃

    // ㅽ誘몃 蹂 荑⑦
    [SerializeField]
    float spCooldown;
    float currentSpCooldown;

    //  愿� 蹂
    [SerializeField]
    float lookSensitivity; // 移대 誘쇨
    [SerializeField]
    float cameraRotationLimit; // 移대  怨 媛
    float currentCameraRotation = 0; //  移대  媛


    //  蹂
    bool isGround = true; //  우吏 щ
    bool isRun = false; // щ━怨 吏 щ
    bool isSpUsed = false; // ㅽ誘몃 ъ щ
    bool isDead = false; // �댁 二쎌 щ

    //  而댄щ
    [SerializeField]
    Rigidbody playerRb;
    [SerializeField]
    Collider playerCol;
    [SerializeField]
    Camera theCamera;
    
    //踰 蹂닿 由
    private Dictionary<BuffType, Coroutine> activeBuffs = new Dictionary<BuffType, Coroutine>();

    // 참조 변수
    [SerializeField]
    GunController theGunController;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //�댁 ㅽ 珥湲고
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

    //濡 踰 �⑺ 硫
    //紐⑺: 濡 踰 �⑺怨, 留 대� 媛 踰 쇰㈃ 湲곗〈 寃 以吏ㅺ� 濡  媛 媛깆
    public void ApplyBuff(BuffType buffType, float buffDuration, float multiplier)
    {
        if (activeBuffs.ContainsKey(buffType)) //留 媛 醫瑜 踰媛 대� 깊ㅻ㈃
        {
            //湲곗〈 踰 肄猷⑦ 硫異湲
            StopCoroutine(activeBuffs[buffType]);
        }
        
        //濡 踰 ④낵瑜 �⑺怨 吏媛 愿由ы 肄猷⑦ 
        Coroutine buffCoroutine = StartCoroutine(BuffCoroutine(buffType, buffDuration, multiplier));
        //由ъ 濡 肄猷⑦ �( 媛깆)
        activeBuffs[buffType] = buffCoroutine;
    }

    private IEnumerator BuffCoroutine(BuffType buffType, float buffDuration, float multiplier)
    {
        
        //踰 ④낵 �
        Debug.Log($"{buffType} 踰 � ! 吏 媛: {buffDuration}珥");

        switch (buffType)
        {
            /*
            *= ъ⑺吏 .
            *= multiplier濡   댁 ④낵 以泥⑸ 臾몄媛 諛.
            */
            case (BuffType.AddSpeed): // 利媛 踰
                appliedRunSpeed = runSpeed * multiplier;
                appliedWalkSpeed = walkSpeed * multiplier;
                break;
            case (BuffType.SuperJump): //�  利媛 踰
                appliedJumpForce = jumpForce * multiplier;
                break;
            case (BuffType.HealthRegen): //泥대 蹂 踰
                break;
        }

        //吏� 媛留 湲
        yield return new WaitForSeconds(buffDuration);

        //踰 ④낵 �嫄
        Debug.Log($"{buffType} 踰 醫猷!");
        switch (buffType)
        {
            case (BuffType.AddSpeed): // 利媛 踰 댁
                appliedRunSpeed = runSpeed;
                appliedWalkSpeed = walkSpeed;
                break;
            case (BuffType.SuperJump): //�  利媛 踰 댁
                appliedJumpForce = jumpForce;
                break;
            case (BuffType.HealthRegen): //泥대 蹂 踰 댁
                break;
        }

        //由ъ 대 踰 �蹂 �嫄
        activeBuffs.Remove(buffType);
    }

    // � 
    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            Jump();
    }

    // �
    void Jump()
    {
        playerRb.linearVelocity = transform.up * appliedJumpForce;
    }

    // щ━湲 
    void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentSp > 0)
            Run();
        else if (Input.GetKeyUp(KeyCode.LeftShift) || currentSp <= 0)
            RunCancel();
    }

    // щ━湲
    void Run()
    {
        isRun = true;
        currentSp -= Time.deltaTime;

        currentSpeed = appliedRunSpeed;
    }

    // щ━湲 痍⑥
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

    // ㅽ誘몃 蹂
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

    // �댁닿  우吏  щ 蹂
    void CheckIsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCol.bounds.extents.y + 0.1f);
    }

    // �댁댁 щ щ 異�ν 硫
    // hp媛 0 硫 true 諛
    bool CheckDead()
    {
        if (hp > 0) // hp媛 0 댁대㈃ ( 二쎌쇰㈃)
            return false;
        return true;
    }

    //  移대 �
    void CameraRotation()
    {
        float rotation = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        currentCameraRotation -= rotation;
        currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotation, 0, 0);
    }

    // 醫 罹由� �
    void CharacterRotation()
    {
        float rotation = Input.GetAxisRaw("Mouse X") * lookSensitivity;
        Vector3 characterRotation = new Vector3(0, rotation, 0);

        playerRb.MoveRotation(playerRb.rotation * Quaternion.Euler(characterRotation));
    }

    // �댁 �蹂 대낫대 硫
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
