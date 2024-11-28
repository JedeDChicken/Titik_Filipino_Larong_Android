using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPersistentAudio : MonoBehaviour
{
    void Start()
    {
        // Find any objects tagged as "AudioSource"
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            // Check if the AudioSource is not in the current scene
            if (audioSource.gameObject.scene.name == null) // Indicates it's persistent
            {
                Destroy(audioSource.gameObject); // Destroy the persistent audio source
            }
        }
    }
}