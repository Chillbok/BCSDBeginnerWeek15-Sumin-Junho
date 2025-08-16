using UnityEngine;

public enum SettingEnum
{
    SFX,
    BGM,
    MOUSE_SENS
}

public class SettingType : MonoBehaviour
{
    [Header("설정 타입")]
    [Tooltip("이 슬라이더가 어떤 설정을 건드리는지 선택하세요")]
    [SerializeField]
    public SettingEnum settingType;
}
