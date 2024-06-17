using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music")]
    public AudioClip mainMenuBGM;
    public AudioClip level1BGM;

    [Header("Sound Effects")]
    public AudioClip mainMenuSFX;
    public AudioClip level1SFX;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

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
        }

        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
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
        PlayBGM(mainMenuBGM);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            PlayBGM(mainMenuBGM);
        }
        else if (scene.name == "level1")
        {
            PlayBGM(level1BGM);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip)
            return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void SwitchToMainMenuBGM()
    {
        PlayBGM(mainMenuBGM);
    }

    public void SwitchToLevel1BGM()
    {
        PlayBGM(level1BGM);
    }

    public void PlayMainMenuSFX()
    {
        PlaySFX(mainMenuSFX);
    }

    public void PlayLevel1SFX()
    {
        PlaySFX(level1SFX);
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}