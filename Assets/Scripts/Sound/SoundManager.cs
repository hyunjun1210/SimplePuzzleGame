using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SoundType
{
    None,
    BGM,
    SFX,
    VOICE,
    Length,
}


public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager m_instance = null;
    public static SoundManager Instance => m_instance;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public List<AudioClip> BGM = new List<AudioClip>();
    public List<AudioClip> SFX = new List<AudioClip>();
    public List<AudioClip> VOICE = new List<AudioClip>();

    private List<AudioSource> m_audioSources = new List<AudioSource>();

    public void Play(SoundType soundType, string soundName, bool isLoop = false)
    {
        if (!string.IsNullOrEmpty(soundName) && !(soundType == SoundType.None || soundType == SoundType.Length))
        {
            soundName = soundName.ToLower();
        }
        else
            return;

        AudioClip clip = null;

        switch (soundType)
        {
            case SoundType.BGM:
                var bgm = BGM.Find(x => x.name.ToLower().Equals(soundName));

                if (bgm != null)
                {
                    clip = bgm;
                }
                break;
            case SoundType.SFX:
                var sfx = SFX.Find(x => x.name.ToLower().Equals(soundName));

                if (sfx != null)
                {
                    clip = sfx;
                }
                break;
            case SoundType.VOICE:
                var voice = VOICE.Find(x => x.name.ToLower().Equals(soundName));

                if (voice != null)
                {
                    clip = voice;
                }
                break;

        }

        if (clip == null)
        {
            return;
        }

        AudioSource reuseAudioSource = m_audioSources.FirstOrDefault(x => x.clip == clip && !x.isPlaying);
        if (reuseAudioSource != null)
        {
            reuseAudioSource.Play();
        }
        else
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSources.Add(audioSource);
            audioSource.clip = clip;
            audioSource.Play();
            audioSource.loop = isLoop;
        }

    }

    public void AllStop()
    {
        m_audioSources.ForEach(audioSource =>
        {
            audioSource.Stop();
            Destroy(audioSource);
        });

        m_audioSources.Clear();
    }

    public void Stop(string soundName)
    {
        m_audioSources.RemoveAll(audioSource =>
        {
            if (audioSource.clip != null && audioSource.clip.name.ToLower().Equals(soundName.ToLower()))
            {
                audioSource.Stop();
                Destroy(audioSource);
                return true;
            }
            return false;
        });
    }
}