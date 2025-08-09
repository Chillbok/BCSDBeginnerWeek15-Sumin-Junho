// 이 코드는 게임을 플레이하며 점수를 계산하고, 플레이 시간을 출력하도록 할 예정.
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    float currentPlayTime = 0; //현재 플레이 시간
    float currentScore = 0; //현재 스코어
    float maxScore; //모든 게임 통틀어서 기록해본 최대 스코어

    // 상태 변수
    [Header("시작 여부")]
    public bool isPlay = false;
    [Header("정지 여부")]
    public bool isPaused = false;

    // 시작 경계선
    [Header("시작 경계선")]
    [SerializeField]
    private GameObject startBorder;

    // 참조 변수
    private PlayerController player;
    private CountDownController countDown;

    // 첫 시작 시
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        countDown = FindObjectOfType<CountDownController>();
        StartCoroutine(StartCount());
    }

    // 씬이 로드된 후
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlayScene")
        {
            player = FindObjectOfType<PlayerController>();
            countDown = FindObjectOfType<CountDownController>();
            isPlay = false; isPaused = false;
            currentPlayTime = 0;
            currentScore = 0;
            startBorder.SetActive(true);
            StartCoroutine(StartCount());
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 카운트 다운 시작
    private IEnumerator StartCount()
    {
        countDown.ChangeTxt("3");
        yield return new WaitForSeconds(1);
        countDown.ChangeTxt("2");
        yield return new WaitForSeconds(1);
        countDown.ChangeTxt("1");
        yield return new WaitForSeconds(1);
        countDown.Deactive();
        isPlay = true;
        startBorder.SetActive(false);
        yield return null;
    }

    void Update()
    {
        if (isPlay)
        {
            currentPlayTime += Time.deltaTime; //시간 변수에 시간 추가하기
            AddScore();
            UpdateMaxScore(); //현재 스코어, 기존 최대 스코어 비교 후 출력시키는 메서드

            if (player.GetIsDead())
            {
                SceneManager.LoadScene("GameResultScene");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isPlay = false;
            }
        }
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