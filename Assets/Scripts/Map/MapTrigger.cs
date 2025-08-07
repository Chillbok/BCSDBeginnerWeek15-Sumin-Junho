using UnityEngine;

public class MapTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var map = MapCreateManager.instance.CreateMap();
            map.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200);
        }
    }
}
