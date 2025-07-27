// 이 코드는 게임을 플레이하며 점수를 계산하고, 플레이 시간을 출력하도록 할 예정.
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; //어디에서든 게임매니저를 접근할 수 있는 전역 변수
    float currentPlayTime = 0; //현재 플레이 시간
    float currentScore; //현재 스코어
    float maxScore; //모든 게임 통틀어서 기록해본 최대 스코어

    void Start()
    {

    }

    void Update()
    {
        currentPlayTime += Time.deltaTime; //시간 늘리기
    }

    #region GetMethods
    public float GetCurrentPlayTime() //현재 플레이 시간
    {
        return currentPlayTime;
    }

    public float GetCurrentScore() //현재 플레이 스코어
    {
        return currentScore;
    }

    public float GetMaxScore() //지금까지 기록해본 가장 높은 스코어
    {
        return maxScore;
    }
    #endregion GetMethods
}
