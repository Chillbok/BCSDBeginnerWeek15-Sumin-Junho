using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

enum SettingEnum
{
    SFX,
    BGM,
    MOUSE_SENS
}

public class SettingManager : MonoBehaviour
{
    private GameData gameData;

    [Header("설정 슬라이더")]
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider mouseSensSlider;

    //세팅값 저장하기 위한 딕셔너리
    Dictionary<SettingEnum, float> SettingValue = new Dictionary<SettingEnum, float>();

    void Start()
    {
    }

    private void Update()
    {
        SettingToValue();
    }

    //딕셔너리에 SettingEnum enum 키와 슬라이더 값 value 추가하는 메서드
    void SettingToValue()
    {
        //이후 최적화를 위해 전 값과 비교하고, 전 값과 같으면 할당을 스킵하는 기능을 추가해야 함.
        if (sfxSlider != null)
            SettingValue[SettingEnum.SFX] = sfxSlider.value;
        if (bgmSlider != null)
            SettingValue[SettingEnum.BGM] = bgmSlider.value;
        if (mouseSensSlider != null)
            SettingValue[SettingEnum.MOUSE_SENS] = mouseSensSlider.value;
    }
}
