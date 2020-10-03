using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource_SFX;
    AudioSource audioSource_BGM;

    [Header("Eat Sound")]
    [SerializeField] AudioClip a_Eat;

    [Header("Interact Sound")]
    [SerializeField] AudioClip a_Pop;
    [SerializeField] AudioClip a_Beep;
    [SerializeField] AudioClip a_Click;

    [Header("Over Sound")]
    [SerializeField] AudioClip a_GameOver;


    [Header("BGM Sound")]
    [SerializeField] AudioClip a_BGM;

    void Awake()
    {
        instance = this;
        audioSource_SFX = GetComponent<AudioSource>();
        audioSource_BGM = GameObject.Find("BGM Sound").GetComponent<AudioSource>();

        CheckAudioState();
    }

    void CheckAudioState()
    {
        if (PlayerPrefs.GetString("AudioState", "isActive") == "!isActive")
        {
            SetActiveAllAudio(false);
        }
        else
        {
            PlayBGM();
        }
    }

    public void SetActiveAllAudio(bool isActive)
    {
        if (isActive)
        {
            audioSource_SFX.enabled = true;
            audioSource_BGM.enabled = true;
            PlayBGM();
        }
        else
        {
            audioSource_SFX.enabled = false;
            audioSource_BGM.enabled = false;
        }
    }

    public void PlayBGM()
    {
        audioSource_BGM.clip = a_BGM;
        audioSource_BGM.loop = true;
        audioSource_BGM.volume = 0.5f;
        audioSource_BGM.Play();
    }

    public void PlayEatSound ()
    {
        audioSource_SFX.PlayOneShot(a_Eat, 1f);
    }

    public void PlayInteractSound(string sound)
    {
        switch (sound)
        {
            case "pop":
                audioSource_SFX.PlayOneShot(a_Pop, 0.2f);
                break;
            case "beep":
                audioSource_SFX.PlayOneShot(a_Beep, 0.2f);
                break;
            case "click":
                audioSource_SFX.PlayOneShot(a_Click, 1f);
                break;
        }
    }

    public void PlayGameOverSound()
    {
        audioSource_SFX.PlayOneShot(a_GameOver, 1f);
    }
}
