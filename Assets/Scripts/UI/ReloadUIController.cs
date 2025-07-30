using UnityEngine;

public class ReloadUIController : MonoBehaviour
{
    // ���� ����
    bool isUpdate;

    // ���� ����
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
