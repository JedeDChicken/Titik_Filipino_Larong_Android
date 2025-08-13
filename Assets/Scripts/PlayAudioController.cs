using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioController : MonoBehaviour, Interactable  // Not used?, on NPCController...
{
    [SerializeField] private AudioSource audioSource;

    public void Interact()
    {
        // Debug.Log("Playing Audio...");

        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource is not set for this interactable1");  // ...1 (@ end)
        }
    }
}
