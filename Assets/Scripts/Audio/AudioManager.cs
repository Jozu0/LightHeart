using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("---------------Audio Sources---------------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;
    [SerializeField] public AudioSource walkingSource;
    [SerializeField] public AudioSource SFXLoopSource;
    [SerializeField] public AudioMixer audioMixer;

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
            if (sceneName == "MainGameScene")
            {
                PlayMusic(sceneName,menuMusic);

            }else if (sceneName == "Adam")
            {
                PlayMusic(sceneName,backgroundNoise);
            }
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute ;
    }
    
    public void PlayMusic(string sceneName,AudioClip clip )
    {
        if (musicSource != null)
        {
                musicSource.clip = clip;
                musicSource.loop = true; // Set the music to loop
                musicSource.Play();
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
        walkingSource.clip = clip;
        walkingSource.loop = true;
        walkingSource.Play();
    }

    public void StopWalking()
    {
        walkingSource.Stop();
    }

    public void PlaySfxLoop(AudioClip clip)
    {
        SFXLoopSource.clip = clip;
        SFXLoopSource.loop = true;
        SFXLoopSource.Play();
    }
}
