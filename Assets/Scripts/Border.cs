using UnityEngine;

public class Border : MonoBehaviour
{
	// 경계선 속도
	[SerializeField]
	private float borderSpeed;

	void Update()
	{
		BorderMove();
	}

	// 경계선 움직임
	void BorderMove()
	{
		transform.Translate(Vector3.forward * borderSpeed * Time.deltaTime);
	}

	// 플랫폼과 충돌 시
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
			other.GetComponent<Rigidbody>().useGravity = true;
			other.GetComponent<Rigidbody>().isKinematic = false;

			Destroy(other, 2f);
        }
    }
}
