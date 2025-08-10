using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // 즉발 데미지
    [Header("즉발 데미지")]
    [SerializeField]
    protected float enterDamage;
    // 지속 데미지
    [Header("지속 데미지")]
    [SerializeField]
    protected float stayDamage;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.DecreaseHP(enterDamage);
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.DecreaseHP(stayDamage);
        }
    }
}
