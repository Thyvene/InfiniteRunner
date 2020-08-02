using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource move_Audio_Source;
    public AudioSource jump_Audio_Source;
    public AudioSource powerUp_Die_AudioSource;
    public AudioSource background_Audio_Source;

    public AudioClip power_Up_Clip;
    public AudioClip die_Clip;
    public AudioClip coin_Clip;
    public AudioClip game_Over_Clip;

    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        // Test if we should play background sound
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    public void PlayMoveLineSound()
    {
        move_Audio_Source.Play();
    }

    public void PlayJumpSound()
    {
        jump_Audio_Source.Play();
    }

    public void PlayDeadSound()
    {
        powerUp_Die_AudioSource.clip = die_Clip;
        powerUp_Die_AudioSource.Play();
    }

    public void PlayPowerUpSound()
    {
        powerUp_Die_AudioSource.clip = power_Up_Clip;
        powerUp_Die_AudioSource.Play();
    }

    public void PlayCoinSound()
    {
        powerUp_Die_AudioSource.clip = coin_Clip;
        powerUp_Die_AudioSource.Play();
    }

    public void PlayeGameOverSound()
    {
        background_Audio_Source.Stop();
        background_Audio_Source.clip = game_Over_Clip;
        background_Audio_Source.loop = false;
        background_Audio_Source.Play();
    }
}
