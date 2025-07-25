using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �÷��̾��� �ӵ�
    public float walkSpeed; // �ȱ� �ӵ�
    public float runSpeed; // �޸��� �ӵ�
    public float applySpeed; // ���� �ӵ�

    // �÷��̾��� ���� ����
    public float jumpForce;

    //�÷��̾��� ü��
    [SerializeField]
    float hp; // �ִ� ü��
    public float currentHp; // ���� ü��

    // �÷��̾��� ���׹̳�
    [SerializeField]
    float sp; // �ִ� ���׹̳� (5�ϰ�� 5�ʵ��� ��� ����)
    float currentSp; // ���� ���׹̳�

    // ���׹̳� ȸ�� ��Ÿ��
    [SerializeField]
    float spCooldown;
    float currentSpCooldown;

    // �þ� ���� ����
    [SerializeField]
    float lookSensitivity; // ī�޶� �ΰ���
    [SerializeField]
    float cameraRotationLimit; // ī�޶� ���� �Ѱ� ����
    float currentCameraRotation = 0; // ���� ī�޶� ���� ����


    // ���� ����
    bool isGround = true; // ���� ��Ҵ��� ����
    bool isRun = false; // �޸��� �ִ��� ����
    bool isSpUsed = false; // ���׹̳� ��� ����
    bool isDead = false; // �÷��̾� ���� ����

    // �ʿ��� ������Ʈ
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

    // ���� �õ�
    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            Jump();
    }

    // ����
    void Jump()
    {
        playerRb.linearVelocity = transform.up * jumpForce;
    }

    // �޸��� �õ�
    void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentSp > 0)
            Run();
        else if (Input.GetKeyUp(KeyCode.LeftShift) || currentSp <= 0)
            RunCancel();
    }

    // �޸���
    void Run()
    {
        isRun = true;
        currentSp -= Time.deltaTime;

        applySpeed = runSpeed;
    }

    // �޸��� ���
    void RunCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    // ������
    void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 velocity = (transform.right * moveDirX + transform.forward * moveDirZ).normalized * applySpeed;

        playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    // ���׹̳� ȸ��
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

    // �÷��̾ ���� ����ִ����� ���� ���� �Ǻ�
    void CheckIsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCol.bounds.extents.y + 0.1f);
    }

    // �÷��̾��� ��� ���� ����ϴ� �޼���
    // hp�� 0�� �Ǹ� true ��ȯ
    bool CheckDead()
    {
        if (hp > 0) // hp�� 0 �̻��̸� (�� �׾�����)
            return false;
        return true;
    }

    // ���� ī�޶� ȸ��
    void CameraRotation()
    {
        float rotation = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        currentCameraRotation -= rotation;
        currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotation, 0, 0);
    }

    // �¿� ĳ���� ȸ��
    void CharacterRotation()
    {
        float rotation = Input.GetAxisRaw("Mouse X") * lookSensitivity;
        Vector3 characterRotation = new Vector3(0, rotation, 0);

        playerRb.MoveRotation(playerRb.rotation * Quaternion.Euler(characterRotation));
    }

    // �÷��̾� ���� �������� �޼����
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
