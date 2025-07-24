using UnityEngine;

public enum BuffType
{
    AddSpeed, //속도 증가 버프
    SuperJump, //점프 높이 증가 버프
    HealthRegen //체력 서서히 회복시키는 버프
}

public class BuffController : MonoBehaviour
{
    public BuffType buffType; //열거형 변수 선언
    public float duration; //버프 지속시간

    void Start()
    {

    }

    void ApplyBuffEffect()
    {
        switch (buffType)
        {
            case BuffType.AddSpeed: //플레이어의 속도를 증가시키는 코드
                Debug.Log("속도 증가 버프 적용!");
                break;
            case BuffType.SuperJump: //플레이어의 점프력을 높이는 코드
                Debug.Log("점프 높이 증가 버프 적용!");
                break;
            case BuffType.HealthRegen: //플레이어의 체력을 회복시키는 코드
                Debug.Log("체력 회복 버프적용!");
                break;
        }
    }
}
