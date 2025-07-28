using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunStatusUIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI currentBulletUI; //현재 총알 개수(UI)
    [SerializeField]
    TextMeshProUGUI leftBulletUI; //남은 총알 개수(UI)

    //가져올 변수
    [SerializeField]
    Gun theGun;

    //총알 개수 가져올 수 있는 스크립트 필요

    //총알 정보
    int currentBulletStatus; //현재 탄창 속 총알 정보
    int leftBulletStatus; //탄창 속 빼고 남은 총알 정보
    int allBulletStatus; //모든 총알 정보

    void Update()
    {
        UpdateBulletCount();
        UpdateBulletUI();
    }

    void UpdateBulletCount() //총알 개수 동기화
    {
        currentBulletStatus = theGun.currentBulletCount;
        leftBulletStatus = theGun.leftBulletCount;
    }

    void UpdateBulletUI() //총알 UI 동기화
    {
        currentBulletUI.text = $"{currentBulletStatus}";
        leftBulletUI.text = $"{leftBulletStatus}";
    }
}
