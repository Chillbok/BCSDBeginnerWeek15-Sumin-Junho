using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알 사거리
    [SerializeField]
    float range;
    //총알 분산도
    [SerializeField]
    float spread;

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
        Vector3 shotSpread = Random.insideUnitSphere * spread;

        bulletRb.AddForce((theCamera.gameObject.transform.forward * range) + shotSpread, ForceMode.Impulse);
        Destroy(gameObject, 2);
    }
}
