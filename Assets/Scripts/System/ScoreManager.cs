using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    //참조 변수
    GameManager gameManager;
    public static int currentScore;
    public static int maxScore;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();

        if (PlayerPrefs.HasKey("MaxScore"))
        {
            maxScore = PlayerPrefs.GetInt("MaxScore");
        }
        else
        {
            maxScore = 0;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        ChangeMaxScore();
    }

    //현재 점수, 최고 점수 불러오기
    void UpdateScore()
    {
        currentScore = (int)gameManager.GetCurrentScore();
        maxScore = (int)gameManager.GetMaxScore();
    }

    //게임 오버 단계에서 실행되는 최고 점수 갱신 메서드
    void ChangeMaxScore()
    {
        //게임 오버가 아닌 경우에 아래 내용 스킵
        if (!gameManager.isGameOver)
            return;

        UpdateScore(); //스코어 동기화
        maxScore = currentScore;
        SaveScore(); //점수 저장
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("MaxScore", maxScore);
    }
}
