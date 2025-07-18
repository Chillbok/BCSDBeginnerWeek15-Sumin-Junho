using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatusBarController : MonoBehaviour
{
    // HP 관련 변수
    [SerializeField]
    GameObject HpBar; // HP 바
    [SerializeField]
    TextMeshProUGUI HpText; // HP 텍스트

    // SP 관련 변수
    [SerializeField]
    GameObject SpBar; //스태미나 바

    // 참조 변수
    [SerializeField]
    PlayerController thePlayerController; //PlayerController.cs 참조 변수

    // PlayerController에서 가져올 변수
    private float currentHp;
    private float hp;
    private float currentSp;
    private float sp;

    void Update()
    {
        UpdatePlayerStatus(); //플레이어 데이터 동기화
        UpdateHpText(); //HP 텍스트 업데이트
    }

    void UpdatePlayerStatus() //플레이어 데이터 가져오기(프레임마다 실행)
    {
        currentHp = thePlayerController.GetPlayerCurrentHP();
        hp = thePlayerController.GetPlayerHP();
        currentSp = thePlayerController.GetPlayerCurrentSP();
        sp = thePlayerController.GetPlayerSP();
    }

    void UpdateHpText()
    {
        HpText.text = $"{currentHp} / {hp}";
    }
}
