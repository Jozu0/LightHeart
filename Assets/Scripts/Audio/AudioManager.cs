using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("---------------Audio Sources---------------")]
    [SerializeField] public AudioSource MusicSource;
    [SerializeField] public AudioSource SFXSource;
    [SerializeField] public AudioSource WalkingSource;
    [SerializeField] public AudioSource SFXLoopSource;
    [SerializeField] public AudioMixer AudioMixer;

    [Header("---------------Audio Clip---------------")]
    public AudioClip menuMusic;
    public AudioClip backgroundNoise;
    public AudioClip death;
    public AudioClip walk;
    public AudioClip damageTakenCube;
    public AudioClip swordAttack;
    public AudioClip swordHeal;
    public AudioClip colorChangeCube;
    public AudioClip enemyDying;
    public AudioClip enemyAttack;
    public AudioClip pushCube;
    public AudioClip enemyGetHit;
    public AudioClip enemyClap;
    public AudioClip randomScaryNoise1;
    public AudioClip randomScaryNoise2;
    public AudioClip doorOpen;
    public AudioClip pressurePlateActivate;
    public AudioClip caveNoise; 
    
    private string currentScene = "";


    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre les scènes
        }
        else
        {
            Destroy(gameObject); // Évite les doublons si une autre instance existe
        }
    }

    
    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != currentScene)
        {
            currentScene = sceneName;
            if (sceneName == "MainMenuScene")
            {
                PlayMusic(sceneName,menuMusic);

            }else
            {
                PlayMusic(sceneName,backgroundNoise);
            }
        }
    }

    public void ToggleMusic()
    {
        MusicSource.mute = !MusicSource.mute ;
    }
    
    public void PlayMusic(string sceneName,AudioClip clip )
    {
        if (MusicSource != null)
        {
                MusicSource.clip = clip;
                MusicSource.loop = true; // Set the music to loop
                MusicSource.Play();
        }               
        else
        {
            Debug.LogWarning("MusicSource ou Backgroundmusic n'est pas assigné !");
        }
    }

    public void PlaySfx(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayWalk(AudioClip clip)
    {
        WalkingSource.clip = clip;
        WalkingSource.loop = true;
        WalkingSource.Play();
    }

    public void StopWalking()
    {
        WalkingSource.Stop();
    }

    public void PlaySfxLoop(AudioClip clip)
    {
        SFXLoopSource.clip = clip;
        SFXLoopSource.loop = true;
        SFXLoopSource.Play();
    }
    
    public void PauseSfxLoop()
    {
        SFXLoopSource.Pause();
    }

    public void UnPauseSfxLoop()
    {
        SFXLoopSource.UnPause();
    }

    public void PlayLocalizedSFX(AudioClip clip)
    {
        
    }
}
