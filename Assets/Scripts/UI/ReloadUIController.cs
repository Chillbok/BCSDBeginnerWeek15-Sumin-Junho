using UnityEngine;

public class ReloadUIController : MonoBehaviour
{
    // 상태 변수
    bool isUpdate;

    // 참조 변수
    [SerializeField]
    GameObject reloadImage;

    void Update()
    {
        CheckReload();
    }

    void CheckReload()
    {
        if (GunController.isReload)
            reloadImage.SetActive(true);
        else
            reloadImage.SetActive(false);
    }

}
