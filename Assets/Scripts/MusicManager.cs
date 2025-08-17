//노래의 출력을 관리함
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singleton<MusicManager>
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private GameManager gameManager;

    [Header("음악 목록")]
    public AudioClip[] gamePlayMusic;

    [Header("오디오 믹서")]
    [SerializeField]
    private AudioMixer audioMixer;

    GameData gameData;
    private float musicVolume;
    private float lastVolume;

    void Start()
    {
        gameData = GameManager.Instance.gameData;
        lastVolume = 0f;
        SetVariableOfMusicVolume();
    }

    void Update()
    {
        SetVariableOfMusicVolume();
        //볼륨 조절 후 게임 데이터 저장
        SaveGame.SaveData(gameData);
    }

    //씬 로드 완료 시
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //오브젝트 비활성화 시
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //씬 로드 완료되면 호출
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChooseBGM(scene.name);
    }

    // BGM 재생(씬이 로드될 때마다 호출)
    void ChooseBGM(string sceneName)
    {
        if (sceneName == gameManager.nameOfPlayScene)
        {
            if (gamePlayMusic != null) PlayLoopMusic(gamePlayMusic[0]);
        }
        else if (sceneName == gameManager.nameOfResultScene)
            audioSource.Stop();
    }

    //볼륨 동기화
    void SetVariableOfMusicVolume()
    {
        musicVolume = -80f + 0.8f * gameData.bgmVolume;
        //Debug.Log($"현재 gameData.bgmVolume {gameData.bgmVolume}, 계산된 musicVolume {musicVolume}");
        if (musicVolume != lastVolume)
        {
            //Debug.Log("볼륨 값 변경 감지. 관련 함수 호출");
            ChangeAudioSourceVolume();
            lastVolume = musicVolume;
        }
    }

    //실제로 볼륨 수정
    void ChangeAudioSourceVolume()
    {
        audioMixer.SetFloat("bgmVolume", musicVolume);
    }

    //시작부분 한번 연주하고, 계속 반복해서 뒷부분 연주
    IEnumerator PlayIntroLoopMusic(AudioClip introMusic, AudioClip loopMusic)
    {
        audioSource.Stop();
        //시작부분 한번만 연주
        Debug.Log("시작 부분 연주 시작");
        audioSource.clip = introMusic;
        audioSource.loop = false;
        audioSource.Play();

        //시작부분 음악만큼 기다리기
        yield return new WaitForSeconds(introMusic.length - 1.2f);

        //반복 연주 부분 계속해서 연주하기
        Debug.Log("반복 부분 연주 시작");
        audioSource.clip = loopMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    //시작부분 없이 하나만 반복해서 연주하기
    void PlayLoopMusic(AudioClip clip)
    {
        Debug.Log("반복음악 연주!");
        audioSource.Stop();
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else Debug.LogWarning("클립이 비어있음!");
    }
}
