//노래의 출력을 관리함
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singleton<MusicManager>
{
    [SerializeField]
    private AudioSource audioSource;

    [Header("음악 목록")]

    public AudioClip[] gameStartMusic;
    public AudioClip[] gamePlayMusic;
    public AudioClip[] gameResultMusic;

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
        switch (sceneName)
        {
            //case "GameStartScene":
            case "GameStartScene_withMusic":
                if (gameStartMusic != null)
                {
                    if (gameStartMusic.Length >= 2) StartCoroutine(PlayIntroLoopMusic(gameStartMusic[0], gameStartMusic[1]));
                }
                break;
            case "GamePlayScene":
                if (gameStartMusic != null)
                {
                    if (gamePlayMusic != null) PlayLoopMusic(gamePlayMusic[0]);
                }
                break;
            case "GameResultScene":
                audioSource.Stop();
                break;
        }
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
