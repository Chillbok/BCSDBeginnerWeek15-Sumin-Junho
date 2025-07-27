using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; //어디에서든 게임매니저를 접근할 수 있는 전역 변수
    float currentPlayTime = 0; //현재 플레이 시간

    void Start()
    {

    }

    void Update()
    {

    }

    #region GetMethods
    public float GetCurrentPlayTime()
    {
        return currentPlayTime;
    }
    #endregion GetMethods
}
