using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 감지 범위
    [SerializeField]
    private float radius;

    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log("플레이어 감지");
            }
        }
    }
}
