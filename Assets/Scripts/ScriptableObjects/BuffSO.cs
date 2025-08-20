using UnityEngine;

public enum BuffType
{
    AddSpeed, //속도 증가 버프
    SuperJump, //점프 높이 증가 버프
    HealthRegen //체력 서서히 회복시키는 버프
}

[CreateAssetMenu(fileName = "BuffScriptableObject", menuName = "Scriptable Objects/BuffScriptableObject", order = int.MaxValue)]
public class BuffSO : ScriptableObject
{
    [Header("버프 데이터")]
    [Tooltip("버프의 종류를 선택")]
    [SerializeField]
    private BuffType _type;
    public BuffType type { get { return _type; } }

    [Tooltip("버프 지속시간")]
    [SerializeField]
    private float _duration = 0;
    public float duration { get { return _duration; } }

    [Tooltip("버프 합연산 값")]
    [SerializeField]
    private float _adder = 0;
    public float adder { get { return _adder; } }

    [Tooltip("버프 업그레이드 곱연산 값")]
    [SerializeField]
    private float _multiplier = 1;
    public float multiplier {get { return _multiplier; }}
}
