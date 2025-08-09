using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    // 카운트 다운 텍스트
    [SerializeField]
    TextMeshProUGUI countdownTxt;

    private void Start()
    {
        if (!GameManager.instance.isPlay)
            countdownTxt.gameObject.SetActive(true);
        else
            countdownTxt.gameObject.SetActive(false);
    }

    // 텍스트 수정
    public void ChangeTxt(string num)
    {
        countdownTxt.text = num;
    }

    // 카운트 다운 비활성화
    public void Deactive()
    {
        countdownTxt.gameObject.SetActive(false);
    }
}
