using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    [SerializeField] Sound[] bgm = null;
    [SerializeField] Sound[] sfx = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;
    private void Update()
    {
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            if (!sfxPlayer[i].isPlaying)
                sfxPlayer[i].clip =null;
        }
        if (!bgmPlayer.isPlaying)
        {
            bgmPlayer.Play();
        }
    }
    public void PlayBGM(string _bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (_bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string _sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (_sfxName == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if (sfx[i].clip == sfxPlayer[j].clip)
                        return;
                }
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                return;
            }
        }
        Debug.Log("잘못된 음악 정보입니다.");
    }
}
