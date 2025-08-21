//버프 아이템에 붙일 스크립트
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    [Header("버프 타입 스크립터블 데이터")]
    [SerializeField]
    private BuffSO buffSO;

    void Start()
    {
        if (buffSO == null)
            Debug.LogError($"{this.name}에 버프 데이터 비어있음!");
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
                    player.ApplyBuff(buffSO.type, buffSO.duration, buffSO.adder, buffSO.multiplier);
                }

                gameObject.SetActive(false); //버프 활성화 패널 비활성화
            }
        }
    }
}
