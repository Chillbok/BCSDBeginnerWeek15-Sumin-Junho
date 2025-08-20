using UnityEngine;

[CreateAssetMenu(fileName = "TurretScriptObject", menuName = "Scriptable Objects/TurretScriptableObject")]
public class TurretSO : ScriptableObject
{
    //주요 스테이터스
    [Header("주요 스테이터스")]

    [Tooltip("포탑 체력")]
    [SerializeField]
    private float _hp;
    public float HP { get { return _hp; } }

    //공격 관련 스탯
    [Header("공격 관련 스탯")]

    [Tooltip("공격 속도: 공격이 나가는 빈도수를 결정함")]
    [SerializeField]
    private float _attackSpeed;
    public float AttackSpeed { get { return _attackSpeed; } }

    [Tooltip("회전 속도: 공격 대상으로 포탑 돌리는 속도")]
    [SerializeField]
    private float _towerRotationSpeed;
    public float TowerRotationSpeed { get { return _towerRotationSpeed; } }

    [Tooltip("총알 속도: 총알이 적에게 날아가는 속도")]
    [SerializeField]
    private float _bulletSpeed;
    public float BulletSpeed{ get { return _bulletSpeed; } }

    //플레이어 인지 관련 스탯
    [Header("플레이어 인지 관련 스탯")]

    [Tooltip("감지 범위: 플레이어를 인식하는 범위를 결정함")]
    [SerializeField]
    private float _attackRange;
    public float AttackRange { get { return _attackRange; } }
}
