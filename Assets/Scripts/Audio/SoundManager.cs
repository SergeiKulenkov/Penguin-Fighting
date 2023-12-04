using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<Sound> sounds;
    private AudioSource source;

    public static SoundManager Instance { get; private set; }

    public enum SoundTypes { Jump, Hit, Slide, Attack, }

    private void Awake()
    {
        if(Instance == null)
        {    
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        source = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundTypes soundType)
    {
        source.clip = GetAudioClip(soundType);
        source.loop = false;
        if (soundType == SoundTypes.Slide)
        {
            source.loop = true;
        }
        source.Play();
    }

    public void StopPlaying()
    {
        source.Stop();
    }

    private AudioClip GetAudioClip(SoundTypes soundType)
    {
        AudioClip audioClip = null;
        foreach (Sound sound in sounds)
        {
            if (sound.type == soundType)
            {
                audioClip = sound.clip;
                break;
            }
        }

        return audioClip;
    }
}

[System.Serializable]
public class Sound
{
    public SoundManager.SoundTypes type;
    public AudioClip clip;
}