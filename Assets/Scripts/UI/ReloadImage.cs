using UnityEngine;
using UnityEngine.UI;

public class ReloadImage : MonoBehaviour
{
    // 이미지를 조정하기 위한 시간 변수
    float currentTime;

    void OnEnable()
    {
        gameObject.GetComponent<Image>().fillAmount = 0;
        currentTime = 0;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        gameObject.GetComponent<Image>().fillAmount = currentTime / 2;
    }
}
