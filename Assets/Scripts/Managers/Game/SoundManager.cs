using System;
using UnityEngine;

[Serializable]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource, effectSource;
    [SerializeField] private float minMasterVolume, maxMasterVolume;

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = Mathf.Clamp(value,minMasterVolume,maxMasterVolume);
    }
}
