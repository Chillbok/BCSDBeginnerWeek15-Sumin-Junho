using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 총알 발사 거리
    [SerializeField]
    float range;

    // 필요한 컴포넌트
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
