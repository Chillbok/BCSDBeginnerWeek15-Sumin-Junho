using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사운드
[System.Serializable]
public class Sound 
{
    // 사운드의 이름
    public string name;
    // 사운드의 클립
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

    // 브금 사운드 배열
    [SerializeField]
    Sound[] bgms;
    // 브금을 재생할 오디오 소스
    [SerializeField]
    AudioSource audioBgm;

    // 효과음 사운드 배열
    [SerializeField]
    Sound[] sfxs;
    // 효과음을 재생할 오디오 소스
    [SerializeField]
    AudioSource[] audioSfx; // 효과음은 여러개가 재생될 수 있으므로 배열로 선언

    // 브금 재생
    public void PlayBGM(string name)
    {
        for (int i = 0; i < bgms.Length; i++)
        {
            if (name == bgms[i].name)
            {
                audioBgm.clip = bgms[i].clip;
                audioBgm.Play();
                return;
            }
        }
    }

    // 재생 중인 효과음
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
