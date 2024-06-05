using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("Audio Clip Player")]
    public AudioClip background;
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
    public AudioClip idleEnemydeath;
    public AudioClip idleEnemyRangeDeath;
    public AudioClip idleEnemyhurt;
    public AudioClip idleEnemyhurtRange;

    void Start(){
        musicSource.clip =background;
        musicSource.Play();
    }
    public void playSfx(AudioClip clip){
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
}