using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SettingEnum
{
    MAX_SCORE = 0,
    SFX = 1,
    BGM = 2,
    MOUSE_SENS = 3
}

[System.Serializable]
public class GameData
{
    public int maxScore = 0; //최대 점수
    public float sfxVolume = 100; //효과음 볼륨
    public float bgmVolume = 100; //배경음악 볼륨
    public float mouseSensitivity = 1; //마우스 감도
}