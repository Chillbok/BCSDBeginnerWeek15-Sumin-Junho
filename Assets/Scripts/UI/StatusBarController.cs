using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarController : MonoBehaviour
{
	// HP 관련 변수
	[SerializeField]
	Image HpBar; // HP 바
	[SerializeField]
	TextMeshProUGUI HpText; // HP 텍스트

	// SP 관련 변수
	[SerializeField]
	Image SpBar; //스태미나 바

	// 참조 변수
	[SerializeField]
	PlayerController thePlayerController; //PlayerController.cs 참조 변수

	// PlayerController에서 가져올 변수
	float currentHp;
	float hp;
	float currentSp;
	float sp;

	void Update()
	{
		UpdatePlayerStatus(); //플레이어 데이터 동기화
		UpdateHpText(); //HP 텍스트 업데이트
		UpdateHpBar(); // HP 바 업데이트
		UpdateSpBar(); // SP 바 업데이트
	}

	//플레이어 데이터 가져오기(프레임마다 실행)
	void UpdatePlayerStatus()
	{
		currentHp = thePlayerController.GetPlayerCurrentHP();
		hp = thePlayerController.GetPlayerHP();
		currentSp = thePlayerController.GetPlayerCurrentSP();
		sp = thePlayerController.GetPlayerSP();
	}

	// HP 텍스트 업데이트
	void UpdateHpText()
	{
		HpText.text = $"{currentHp} / {hp}";
	}

	// HP 바 업데이트
	void UpdateHpBar()
	{
		HpBar.fillAmount = currentHp / hp;
	}

	// SP 바 업데이트
	void UpdateSpBar()
	{
		SpBar.fillAmount = currentSp / sp;
	}
}
