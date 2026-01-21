using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
public enum AudioFx
{

}

public enum AudioMusic
{

}

public enum AudioAmbience
{

}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip[] fxClips;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] ambienceClips;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource ambienceAudioSource;
    [SerializeField] private AudioSource fxAudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            ambienceAudioSource.volume = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
            fxAudioSource.volume = PlayerPrefs.GetFloat("FXVolume", 1f);
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayAudioClip(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayFx(AudioFx audioFx)
    {
        fxAudioSource.PlayOneShot(fxClips[(int)audioFx]);
    }

    public void PlayMusic(AudioMusic audioMusic, bool isLooping = true)
    {
        musicAudioSource.loop = isLooping;
        musicAudioSource.clip = musicClips[(int)audioMusic];
        musicAudioSource.Play();
    }

    public void PlayAmbience(AudioAmbience audioAmbience, bool isLooping = true)
    {
        ambienceAudioSource.loop = isLooping;
        ambienceAudioSource.clip = ambienceClips[(int)audioAmbience];
        ambienceAudioSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;  
    }

    public void SetAmbienceVolume(float volume)
    {
        ambienceAudioSource.volume = volume;  
    }

    public void SetFxVolume(float volume)
    {
        fxAudioSource.volume = volume;  
    }

    public float GetMusicVolume()
    {
        return musicAudioSource.volume;  
    }

    public float GetAmbienceVolume()
    {
        return ambienceAudioSource.volume;  
    }

    public float GetFxVolume()
    {
        return fxAudioSource.volume; 
    }
    public Coroutine FadeOutMusic(float duration)
    {
        return StartCoroutine(FadeOutMusicCoroutine(duration));
    }

    private IEnumerator FadeOutMusicCoroutine(float duration)
    {
        float startVolume = musicAudioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            musicAudioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        musicAudioSource.volume = 0f;
        musicAudioSource.Stop();
    }
    public IEnumerator PlayFanfareAndWait(AudioClip fanfareClip)
    {
        fxAudioSource.PlayOneShot(fanfareClip);
        yield return new WaitForSeconds(fanfareClip.length);
    }

}
