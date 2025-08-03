using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // µ¥¹ÌÁö
    [SerializeField]
    protected float damage;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.DecreaseHP(damage);
        }
    }
}
