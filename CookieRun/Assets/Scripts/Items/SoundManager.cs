using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume = 0.5f;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance = 0.5f;
    [SerializeField][Range(0f, 1f)] private float musicVolume = 0.5f;
    [SerializeField][Range(0f, 1f)] private float masterVolume = 0.5f;
    
    public float MasterVolume { get => masterVolume; set => masterVolume = Mathf.Clamp01(value); }

    public float SoundEffectVolume
    { get => soundEffectVolume; set
        {
            soundEffectVolume = Mathf.Clamp01(value);
            if (musicAudioSource != null)
                musicAudioSource.volume = soundEffectVolume;
        }
    }

    public float SoundEffectPitchVariance
    {
        get => soundEffectPitchVariance; set
        {
            soundEffectPitchVariance = Mathf.Clamp01(value);
            if (musicAudioSource != null)
                musicAudioSource.volume = soundEffectPitchVariance;
        }
    }
    public float MusicVolume
    { get => musicVolume; set
        {
            musicVolume = Mathf.Clamp01(value);
            if (musicAudioSource != null)
                musicAudioSource.volume = musicVolume;
        }
    }
    
    private AudioSource musicAudioSource;
    public AudioClip musicClip;

    public SoundSource soundSourcePrefab;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }
    
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(Instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, Instance.soundEffectVolume, Instance.soundEffectPitchVariance); 
    }
}
