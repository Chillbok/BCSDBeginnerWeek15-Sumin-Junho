using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    // 카운트 다운 텍스트
    [SerializeField]
    TextMeshProUGUI countdownTxt;
    // 크로스헤어
    [SerializeField]
    GameObject crosshair;

    private void Start()
    {
        if (!GameManager.Instance.isPlay)
        {
            crosshair.SetActive(false);
            countdownTxt.gameObject.SetActive(true);
        }
        else
        {
            crosshair.SetActive(true);
            countdownTxt.gameObject.SetActive(false);
        }
    }

    // 텍스트 수정
    public void ChangeTxt(string num)
    {
        countdownTxt.text = num;
    }

    // 카운트 다운 비활성화
    public void Deactive()
    {
        crosshair.SetActive(true);
        countdownTxt.gameObject.SetActive(false);
    }
}
