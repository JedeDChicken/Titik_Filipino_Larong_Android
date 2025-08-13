using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPersistentAudio : MonoBehaviour  // Not used?, On other scenes except Home...
{
    void Start()
    {
        // Find any objects tagged as "AudioSource"
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();  // AudioSource (object type?), [] (to array), allAudioSources (variable name)

        foreach (AudioSource audioSource in allAudioSources)
        {
            // Check if the AudioSource is not in the current scene
            if (audioSource.gameObject.scene.name == null)  // Indicates it's persistent, object isn't part of currently loaded scene
            {
                Destroy(audioSource.gameObject);  // Destroy the persistent audio source
            }
        }
    }
}