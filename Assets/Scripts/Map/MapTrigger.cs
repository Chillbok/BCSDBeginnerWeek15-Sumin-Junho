using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    private static Map previousMap;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var map = MapCreateManager.Instance.CreateMap();
            map.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200);

            if (previousMap != null)
                previousMap.DestroyMap();

            previousMap = GetComponentInParent<Map>();

            gameObject.SetActive(false);
        }
    }
}
