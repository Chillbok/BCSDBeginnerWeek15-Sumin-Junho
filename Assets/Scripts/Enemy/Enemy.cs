using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ���� ����
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
                Debug.Log("�÷��̾� ����");
            }
        }
    }
}
