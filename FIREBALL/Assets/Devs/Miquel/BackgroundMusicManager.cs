using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip musicClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetupMusic();
    }

    private void SetupMusic()
    {
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = musicClip;
        audioSource.loop = true;        
        audioSource.playOnAwake = true; 
        audioSource.volume = 1f;        
        audioSource.Play();
    }
}
