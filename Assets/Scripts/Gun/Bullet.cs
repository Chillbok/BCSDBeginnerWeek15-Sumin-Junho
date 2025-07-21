using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �Ѿ� �߻� �Ÿ�
    [SerializeField]
    float range;

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
        bulletRb.AddForce(theCamera.gameObject.transform.forward * range, ForceMode.Impulse);
        Destroy(gameObject, 2);
    }
}
