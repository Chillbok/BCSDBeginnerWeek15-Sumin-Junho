using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �÷��̾��� �ӵ�
    [SerializeField]
    private float walkSpeed; // �ȱ� �ӵ�
    [SerializeField]
    private float runSpeed; // �޸��� �ӵ�
    private float applySpeed; // ���� �ӵ�

    // �÷��̾��� ���� ����
    [SerializeField]
    private float jumpForce;

    //�÷��̾��� ü��
    [SerializeField]
    private float hp; // �ִ� ü��
    private float currentHp; // ���� ü��

    // �÷��̾��� ���׹̳�
    [SerializeField]
    private float sp; // �ִ� ���׹̳� (5�ϰ�� 5�ʵ��� ��� ����)
    private float currentSp; // ���� ���׹̳�

    // ���׹̳� ȸ�� ��Ÿ��
    [SerializeField]
    private float spCooldown;
    private float currentSpCooldown;

    // ���� ����
    private bool isGround = true; // ���� ��Ҵ��� ����
    private bool isRun = false; // �޸��� �ִ��� ����
    private bool isSpUsed = false; // ���׹̳� ��� ����
    private bool isDead = false; // �÷��̾� ���� ����

    // �ʿ��� ������Ʈ
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

    // �޸��� �õ�
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentSp > 0)
            Run();
        else if (Input.GetKeyUp(KeyCode.LeftShift) || currentSp <= 0)
            RunCancel();
    }

    // �޸���
    private void Run()
    {
        isRun = true;
        currentSp -= Time.deltaTime;

        applySpeed = runSpeed;
    }

    // �޸��� ���
    private void RunCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    // ������
    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 velocity = new Vector3(moveDirX, 0, moveDirZ).normalized * applySpeed;

        playerRb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    // ���׹̳� ȸ��
    private void SPRecover()
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

    // �÷��̾ ���� ����ִ����� ���� ���� �Ǻ�
    private void CheckIsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCol.bounds.extents.y + 0.1f);
    }

    // �÷��̾��� ��� ���� ����ϴ� �޼���
    // hp�� 0�� �Ǹ� true ��ȯ
    private bool CheckDead()
    {
        if (hp > 0) // hp�� 0 �̻��̸� (�� �׾�����)
            return false;
        return true;
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
