using UnityEngine;
using UnityEngine.UI;

public class ReloadImage : MonoBehaviour
{
    // �̹����� �����ϱ� ���� �ð� ����
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
