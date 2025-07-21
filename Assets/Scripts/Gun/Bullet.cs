using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �Ѿ� �߻� �Ÿ�
    [SerializeField]
    float range;
    // �Ѿ� ��ź ����
    [SerializeField]
    float spread;

    // �ʿ��� ������Ʈ
    Rigidbody bulletRb;
    [SerializeField]
    Camera theCamera;

    void Awake()
    {
        bulletRb = GetComponent<Rigidbody>();
        theCamera = FindObjectOfType<Camera>();
    }

    void Start()
    {
        Vector3 shotSpread = new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0);

        bulletRb.AddForce((theCamera.gameObject.transform.forward * range) + shotSpread, ForceMode.Impulse);
        Destroy(gameObject, 2);
    }
}
