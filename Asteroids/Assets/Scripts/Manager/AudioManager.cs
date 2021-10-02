using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static bool initialized;
    private static AudioSource audioSource;
    private static Dictionary<AudioName, AudioClip> audioClips = new Dictionary<AudioName, AudioClip>();
    
    public static bool Initialized => initialized;

    public static void Initialize(AudioSource audio)
    {
        initialized = true;
        audioSource = audio;
        audioClips.Add(AudioName.Background, Resources.Load<AudioClip>("background"));
        audioClips.Add(AudioName.Button, Resources.Load<AudioClip>("button"));
        audioClips.Add(AudioName.Rock, Resources.Load<AudioClip>("rock"));
        audioClips.Add(AudioName.Shoot, Resources.Load<AudioClip>("shoot"));
        audioClips.Add(AudioName.GameOver, Resources.Load<AudioClip>("gameOver"));
    }

    public static void Play(AudioName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}