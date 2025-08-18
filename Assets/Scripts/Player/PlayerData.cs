using UnityEngine;

//에디터에서 에셋으로 만들기 위한 속성
[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject //monobehaviour 대신 상속받는 속성
{
    //플레이어 스테이터스
    [Header("플레이어 스테이터스")]
    [Tooltip("플레이어 최대 HP")]
    [SerializeField]
    private float _playerMaxHP;
    public float PlayerMaxHP { get { return _playerMaxHP; } }

    [Header("SP 관련 변수")]
    [Tooltip("플레이어 최대 SP")]
    [SerializeField]
    private float _playerMaxSP;
    public float PlayerMaxSP { get { return _playerMaxSP; } }

    [Tooltip("sp 소모 멈춘 후 다시 회복되기까지 걸리는 시간(초)")]
    [SerializeField]
    private float _playerSpCooldown;
    public float PlayerSpCooldown {get { return _playerSpCooldown; }}



    //플레이어 이동 관련 변수
    [Header("플레이어 이동 관련 변수")]

    [Tooltip("플레이어 걷기 속도")]
    [SerializeField]
    private float _playerWalkSpeed;
    public float PlayerWalkSpeed { get { return _playerWalkSpeed; } }

    [Tooltip("플레이어 달리기 속도")]
    [SerializeField]
    private float _playerRunSpeed;
    public float PlayerRunSpeed { get { return _playerRunSpeed; } }

    [Tooltip("플레이어 점프 강도")]
    [SerializeField]
    private float _playerJumpForce;
    public float PlayerJumpForce { get { return _playerJumpForce; } }

    [Tooltip("카메라 상하 한계 각도")]
    [SerializeField]
    private float _playerCameraRotationLimit;
    public float PlayerCameraRotationLimit { get { return _playerCameraRotationLimit; } }    //상태 변수
}