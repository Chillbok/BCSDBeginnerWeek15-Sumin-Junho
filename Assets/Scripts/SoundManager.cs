using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Sound 
{
    // 사운드 이름
    public string name;
    // 사운드 클립
    public AudioClip clip;
}



public class SoundManager : MonoBehaviour
{
    #region singleton
    public static SoundManager instance;

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

    // BGM 출력할 오디오 소스
    [SerializeField]
    AudioSource audioBgm;
    
    //BGM 목록
    [Header("음악 목록")]
    public AudioClip[] gameStartMusic;
    public AudioClip[] gamePlayMusic;
    public AudioClip[] gameResultMusic;

    // 효과음
    [SerializeField]
    Sound[] sfxs;
    // 효과음 출력할 오디오 소스
    [SerializeField]
    AudioSource[] audioSfx; // 효과음은 중첩되어 들릴 수 있으므로 배열로 설정

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
                    else if (gameStartMusic.Length > 0) PlayLoopMusic(gameStartMusic[0]);
                }
                break;
            case "GamePlayScene":
                if (gameStartMusic != null)
                {
                    if (gamePlayMusic != null) PlayLoopMusic(gamePlayMusic[0]);
                }
                break;
            case "GameResultScene":
                audioBgm.Stop();
                break;
        }
    }

    //시작부분 한번 연주하고, 계속 반복해서 뒷부분 연주
    IEnumerator PlayIntroLoopMusic(AudioClip introMusic, AudioClip loopMusic)
    {
        audioBgm.Stop();
        //시작부분 한번만 연주
        audioBgm.clip = introMusic;
        audioBgm.loop = false;
        audioBgm.Play();

        //시작부분 음악만큼 기다리기
        yield return new WaitForSeconds(introMusic.length - 1.2f);

        //반복 연주 부분 계속해서 연주하기
        audioBgm.clip = loopMusic;
        audioBgm.loop = true;
        audioBgm.Play();
    }

    //시작부분 없이 하나만 반복해서 연주하기
    void PlayLoopMusic(AudioClip clip)
    {
        audioBgm.Stop();
        if (clip != null)
        {
            audioBgm.clip = clip;
            audioBgm.loop = true;
            audioBgm.Play();
        }
        else Debug.LogWarning("클립이 비어있음!");
    }

    // 재생 중인 효과음 이름
    public string[] playSoundName;

    // 효과음 재생
    public void PlaySFX(string name)
    {
        for (int i = 0; i < sfxs.Length; i++)
        {
            if (name == sfxs[i].name)
            {
                for (int j = 0; j < audioSfx.Length; j++)
                {
                    if (!audioSfx[j].isPlaying)
                    {
                        playSoundName[j] = sfxs[i].name;
                        audioSfx[j].clip = sfxs[i].clip;
                        audioSfx[j].Play();
                        return;
                    }
                }
            }
        }
    }

    // 효과음 중지
    public void StopSFX(string name)
    {
        for (int i = 0; i < audioSfx.Length; i++)
        {
            if (playSoundName[i] == name)
            {
                audioSfx[i].Stop();
                return;
            }
        }
    }

    // 모든 효과음 중지
    public void StopAllSFX()
    {
        for (int i = 0; i < audioSfx.Length; i++)
        {
            audioSfx[i].Stop();
        }
    }
}
