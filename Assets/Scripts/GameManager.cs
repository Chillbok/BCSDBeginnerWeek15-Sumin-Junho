// 이 코드는 게임을 플레이하며 점수를 계산하고, 플레이 시간을 출력하도록 할 예정.
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
