using UnityEngine;

public class PlayerSensitivityManager : MonoBehaviour
{
    //게임 데이터
    GameData gameData;

    //플레이어 감도
    float lookSensitivity;
    float lastSensitivity;

    void Start()
    {
        gameData = GameManager.Instance.gameData;
        lookSensitivity = 1f;
    }

    void Update()
    {
        DefineSensitivity();
    }

    //감도 수정 판단 메서드
    void DefineSensitivity()
    {
        if (lookSensitivity != lastSensitivity)
        {
            ChangeSensitivity();
            lastSensitivity = lookSensitivity;
            SaveGame.SaveData(gameData); //수정 후 저장
        }
    }

    //감도 수정
    void ChangeSensitivity()
    {
        lookSensitivity = gameData.mouseSensitivity;
    }
}
