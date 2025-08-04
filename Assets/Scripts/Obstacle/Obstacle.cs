using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // 데미지
    [SerializeField]
    protected float damage;

    protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.DecreaseHP(damage);
        }
    }
}
