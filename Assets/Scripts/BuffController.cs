//버프 아이템에 붙일 스크립트
using System.Runtime.CompilerServices;
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
    [SerializeField]
    public float buffDuration; //버프 지속시간

    //참조변수
    [SerializeField]
    private PlayerController thePlayerController;


    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.CompareTag("Player")) //만약 부딪힌 객체의 태그가 Player라면
            {
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    //PlayerController에 있는 ApplyBuff 메서드 호출해 버프 종류, 지속시간, 강도 전달
                    player.ApplyBuff(this.buffType, this.buffDuration, 1.5f); //1.5f는 예시 비율
                }

                gameObject.SetActive(false); //버프 활성화 패널 비활성화
            }
        }
    }
}
