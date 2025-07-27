using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    //업데이트할 UI 항목들
    [SerializeField]
    TextMeshProUGUI playTimeText; //플레이 시간 텍스트 추가
    [SerializeField]
    TextMeshProUGUI maxScoreText; //최대 스코어 텍스트 추가
    [SerializeField]
    TextMeshProUGUI currentScoreText; //현재 스코어 텍스트 추가

    //참조변수들
    [SerializeField]
    GameManager theGameManager; //GameManager 참조변수

    //변수들
    float playTime; //플레이 시간
    int playTimeMin; //플레이 시간(분)
    int playTimeSec; //플레이 시간(초)
    float currentScore; //현재 플레이 점수
    float maxScore; //최대 플레이 점수

    void Start()
    {

    }

    void Update()
    {
        PlayTimeMinSec(); //플레이 시간 분, 초로 나눠서 구하기
        UpdatePlayTime(); //플레이 시간을 UI에 출력
        UpdateScore(); //플레이 점수 동기화
    }

    //플레이 시간을 분, 초로 나눠서 구하는 메서드
    void PlayTimeMinSec()
    {
        playTime = theGameManager.GetCurrentPlayTime(); //플레이 시간 float값 동기화
        playTimeMin = (int)(playTime / 60f); //분 계산
        playTimeSec = (int)(playTime % 60f); //초 계산
    }

    //플레이 시간을 UI에 출력
    void UpdatePlayTime()
    {
        //플레이 시간 출력
        playTimeText.text = $"{playTimeMin}:{playTimeSec}";
        //현재 점수 출력
        //최대 점수 출력하고, 현재 점수가 더 커질 경우 현재 점수로 대체해서 출력
    }

    //점수를 출력하는 메서드
    void UpdateScore()
    {
        currentScore = theGameManager.GetCurrentScore(); //변수에 현재 점수 동기화
        maxScore = theGameManager.GetMaxScore(); //변수에 최대 점수 동기화
        currentScoreText.text = $"{(int)currentScore}"; //점수를 int형으로 변환해 출력
        maxScoreText.text = $"{(int)maxScore}"; //점수를 int형으로 변환해 출력

        
    }
}
