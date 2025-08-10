// 이 코드는 게임을 플레이하며 점수를 계산하고, 플레이 시간을 출력하도록 할 예정.
//데이터의 관리 또한 담당한다.
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //데이터 관리
    public GameData gameData;

    // 참조 변수
    [Header("참조 변수")]
    private PlayerController player;
    private CountDownController countDown;
    private ScoreManager scoreManager;

    [Header("플레이 점수")]
    [SerializeField]
    float currentPlayTime = 0; //현재 플레이 시간

    // 상태 변수
    [Header("시작 여부")]
    public bool isPlay = false;
    [Header("정지 여부")]
    public bool isPaused = false;
    [Header("게임 오버 여부")]
    public bool isGameOver = false;

    // 시작 경계선
    [Header("시작 경계선")]
    [SerializeField]
    private GameObject startBorder;

    protected override void Awake()
    {
        base.Awake();

        //데이터 로드
        gameData = SaveGame.LoadData();
        if (gameData == null)
        {
            gameData = new GameData();
            gameData.maxScore = 0;
        }
    }

    // 첫 시작 시
    void Start()
    {
    }

    // 씬이 로드된 후
    private void OnEnable()
    {
        player = FindObjectOfType<PlayerController>();
        countDown = FindObjectOfType<CountDownController>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlayScene")
        {
            //씬 불러올 때 참조변수 할당
            player = FindObjectOfType<PlayerController>();
            countDown = FindObjectOfType<CountDownController>();
            TryGetComponent<ScoreManager>(out scoreManager);
            StartCoroutine(StartCount());

            //스코어 최적화
            scoreManager.SetCurrentScore(0f);

            isPlay = true; isPaused = false;
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
        if (isPlay && !isGameOver)
        {
            currentPlayTime += Time.deltaTime; //시간 변수에 시간 추가하기
            scoreManager.AddScore(Time.deltaTime * 10);

            if (player.GetIsDead()) //플레이어가 죽었을 때
            {
                isGameOver = true;
                isPlay = false;

                //스코어 저장을 위해 ScoreManager에 점수 추가
                float finalScore = scoreManager.GetCurrentScore();

                if (finalScore > gameData.maxScore)
                    gameData.maxScore = Mathf.RoundToInt(finalScore);

                SaveGame.SaveData(gameData);

                //게임 결과 씬 불러오기
                SceneManager.LoadScene("GameResultScene");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    //Get 메서드 모음
    #region GetMethods
    public float GetCurrentPlayTime() //현재 플레이 시간
    {
        return currentPlayTime;
    }

    public float GetCurrentScore() //현재 플레이 스코어
    {
        float score = scoreManager.GetCurrentScore();
        return score;
    }

    public float GetMaxScore() //지금까지 기록해본 가장 높은 스코어
    {
        return gameData.maxScore;
    }
    #endregion GetMethods
}