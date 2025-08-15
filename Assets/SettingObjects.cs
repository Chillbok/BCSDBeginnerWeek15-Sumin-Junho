//설정 오브젝트 관리
using UnityEngine;
using UnityEngine.UI;

public class SettingObjects : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider bgmSlider;
    public Slider mouseSensitivity;

    void Start()
    {
        if (sfxSlider == null)
        {
            Debug.LogWarning($"{sfxSlider.name} 배정 필요!");
        }
        if (bgmSlider == null)
        {
            Debug.LogWarning($"{bgmSlider.name} 배정 필요!");
        }
        if (mouseSensitivity == null)
        {
            Debug.LogWarning($"{mouseSensitivity.name} 배정 필요!");
        }
    }
}
