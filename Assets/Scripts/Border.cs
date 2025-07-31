using System.Collections;
using UnityEngine;

public class Border : MonoBehaviour
{
	// 경계선 속도
	[SerializeField]
	private float borderSpeed;

	// 참조 변수
	[SerializeField]
	private PlayerController player;

	void Update()
	{
		BorderMove();
		PlayerDamage();
	}

	// 경계선 움직임
	void BorderMove()
	{
		transform.Translate(Vector3.forward * borderSpeed * Time.deltaTime);
	}

	// 플레이어 데미지 입힘
	void PlayerDamage()
    {
		if (transform.position.z >= player.transform.position.z)
			player.DecreaseHP(0.1f);
    }
}
