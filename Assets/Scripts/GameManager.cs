// 이 코드는 게임을 플레이하며 점수를 계산하고, 플레이 시간을 출력하도록 할 예정.
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion singleton

    [SerializeField]
    float currentPlayTime = 0; //현재 플레이 시간
    float currentScore = 0; //현재 스코어
    float maxScore; //모든 게임 통틀어서 기록해본 최대 스코어

    // 상태 변수
    public bool isPlay = false;
    public bool isPaused = false;

    void Start()
    {
        StartCoroutine(StartCount());
    }

    private IEnumerator StartCount()
    {
        yield return new WaitForSeconds(3);
        isPlay = true;
    }

    void Update()
    {
        currentPlayTime += Time.deltaTime; //시간 변수에 시간 추가하기
        AddScore();
        UpdateMaxScore(); //현재 스코어, 기존 최대 스코어 비교 후 출력시키는 메서드
    }

    //점수 추가하는 메서드
    void AddScore()
    {
        currentScore += Time.deltaTime * 10; //점수에 현재 플레이 시간 * 10 추가
    }

    void UpdateMaxScore() //현재 스코어와 비교해 최대 스코어 반환하는 메서드
    {
        if (maxScore < currentScore)
            maxScore = currentScore;
        return;
    }

    //Get 메서드 모음
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