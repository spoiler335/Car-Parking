using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip driveSound;
    [SerializeField] private AudioClip breakSound;


    private AudioSource audioSource;

    void Start()
    {
        DI.di.SetAudioManager(this);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCrashSound()
    {
        Debug.Log("Playing Crash Sound ");
        audioSource.PlayOneShot(crashSound);
    }

    public void PlayDriveSound()
    {
        Debug.Log("Playing Drive Sound");
        audioSource.PlayOneShot(driveSound);
    }

    public void PlayBreakSound()
    {
        Debug.Log("Playing Break Sound");
        audioSource.PlayOneShot(breakSound);
    }

}
