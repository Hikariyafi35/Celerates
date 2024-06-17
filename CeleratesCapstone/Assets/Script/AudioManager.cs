using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clip Player")]
    public AudioClip mainMenuBGM;
    public AudioClip level1BGM;
    public AudioClip death;
    public AudioClip attack;
    public AudioClip shoot;
    public AudioClip run;
    public AudioClip jump;

    [Header("Audio Clip Enemy")]
    public AudioClip idleEnemyMeele;
    public AudioClip idleEnemyRange;
    public AudioClip idleEnemyEnemyAttack;
    public AudioClip idleEnemyRangeAttack;
    public AudioClip idleEnemyDeath;
    public AudioClip idleEnemyRangeDeath;
    public AudioClip idleEnemyHurt;
    public AudioClip idleEnemyHurtRange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // Memastikan BGM yang sesuai diputar saat start berdasarkan scene aktif
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
        {
            PlayBGM(mainMenuBGM);
        }
        else if (scene.name == "level 1")
        {
            PlayBGM(level1BGM);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (musicSource.clip == clip)
            return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void playSfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayLoopingSfx(AudioClip clip)
    {
        if (sfxSource.clip != clip || !sfxSource.isPlaying)
        {
            sfxSource.clip = clip;
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }

    public void StopSfx(AudioClip clip)
    {
        if (sfxSource.clip == clip && sfxSource.isPlaying)
        {
            sfxSource.loop = false;
            sfxSource.Stop();
            sfxSource.clip = null;
        }
    }

    public bool IsPlaying(AudioClip clip)
    {
        return sfxSource.clip == clip && sfxSource.isPlaying;
    }

    public void SetBGMVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}