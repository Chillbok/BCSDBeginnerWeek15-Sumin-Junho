using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ????
[System.Serializable]
public class Sound 
{
    // ?????? ???
    public string name;
    // ?????? ???
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

    // ??? ???? ?עק
    [SerializeField]
    Sound[] bgms;
    // ????? ????? ????? ???
    [SerializeField]
    AudioSource audioBgm;

    // ????? ???? ?עק
    [SerializeField]
    Sound[] sfxs;
    // ??????? ????? ????? ???
    [SerializeField]
    AudioSource[] audioSfx; // ??????? ???????? ????? ?? ??????? ?עק?? ????

    // ??? ???
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

    // ??? ???? ?????
    public string[] playSoundName;

    // ????? ???
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

    // ????? ????
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

    // ??? ????? ????
    public void StopAllSFX()
    {
        for (int i = 0; i < audioSfx.Length; i++)
        {
            audioSfx[i].Stop();
        }
    }
}
