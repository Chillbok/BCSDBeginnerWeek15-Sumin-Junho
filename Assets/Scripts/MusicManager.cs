//노래의 출력을 관리함
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singleton<MusicManager>
{
    private AudioSource audioSource;

    [Header("음악 목록")]

    public AudioClip[] gameStartMusic;
    public AudioClip[] gamePlayMusic;
    public AudioClip[] gameResultMusic;

    protected override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            //case "GameStartScene":
            case "GameStartScene_withMusic":
                if (gameStartMusic != null)
                {
                    if (gameStartMusic.Length >= 2)
                        StartCoroutine(PlayIntroLoopMusic(gameStartMusic[0], gameStartMusic[1]));
                    else if (gameStartMusic.Length > 0)
                        PlayMusic(gameStartMusic[0]);
                }
                break;
            case "GamePlayScene":
                break;
            case "GameResultScene":
                break;
        }
    }

    //시작부분이 있는 반복 음악
    IEnumerator PlayIntroLoopMusic(AudioClip intro, AudioClip loop)
    {
        //시작부분 한번만 연주
        audioSource.clip = intro;
        audioSource.loop = false;
        audioSource.Play();

        //시작부분 음악만큼 기다리기
        yield return new WaitForSeconds(intro.length);

        //반복 연주 부분 계속해서 연주하기
        audioSource.clip = loop;
        audioSource.loop = true;
        audioSource.Play();
    }

    //시작 부분 없이 반복 음악
    void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
