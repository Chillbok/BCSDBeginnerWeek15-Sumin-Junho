using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����
[System.Serializable]
public class Sound 
{
    // ������ �̸�
    public string name;
    // ������ Ŭ��
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

    // ��� ���� �迭
    [SerializeField]
    Sound[] bgms;
    // ����� ����� ����� �ҽ�
    [SerializeField]
    AudioSource audioBgm;

    // ȿ���� ���� �迭
    [SerializeField]
    Sound[] sfxs;
    // ȿ������ ����� ����� �ҽ�
    [SerializeField]
    AudioSource[] audioSfx; // ȿ������ �������� ����� �� �����Ƿ� �迭�� ����

    // ��� ���
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

    // ��� ���� ȿ����
    public string[] playSoundName;

    // ȿ���� ���
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

    // ȿ���� ����
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

    // ��� ȿ���� ����
    public void StopAllSFX()
    {
        for (int i = 0; i < audioSfx.Length; i++)
        {
            audioSfx[i].Stop();
        }
    }
}
