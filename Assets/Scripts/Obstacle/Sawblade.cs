using UnityEngine;

public class Sawblade : MonoBehaviour
{
    // 데미지
    [SerializeField]
    private float damage;

    // 움직임 속도
    [SerializeField]
    private float speed;

    // 필요한 좌표 벡터
    Vector3 left = new Vector3(-0.25f, 0, 0);
    Vector3 right = new Vector3(1.75f, 0, 0);

    void Update()
    {
        MoveSawblade();
    }

    // 톱니 움직임
    private void MoveSawblade()
    {
        float time = Mathf.PingPong(Time.time * speed, 1.0f);

        transform.localPosition = Vector3.Lerp(left, right, time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.DecreaseHP(damage);
        }
    }
}
