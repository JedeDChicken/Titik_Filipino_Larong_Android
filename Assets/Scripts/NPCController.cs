using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    // [SerializeField] Dialog dialog;
    // [SerializeField] private AudioSource audioSource;
    // public PlayerMovement playerMovement;   //Reference to the PlayerMovement script
    // public bool isBoss;

    // public void Interact()
    // {
    //     // Debug.Log("You will converse");
    //     // StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

    //     Debug.Log("You will converse and hear audio.");

    //     if ((audioSource != null) && !playerMovement.isTalking)
    //     {
    //         audioSource.Play();
    //     }
    //     // else if (audioSource == null)
    //     else
    //     {
    //         Debug.LogError("AudioSource is not set for this interactable.");
    //     }

    //     StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    // }

    [SerializeField] Dialog dialog;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource specificAudioSource;  // For special interactions (e.g. PH Flag)
    public PlayerMovement playerMovement;  // Reference to the PlayerMovement script
    public bool isBoss;
    private AudioSource[] allAudioSources;

    public void Interact()
    {
        if (specificAudioSource != null)
        {
            Debug.Log("You'll have special interaction w/ audio");
            if (!playerMovement.isTalking)
            {
                StartCoroutine(HandleSpecialInteraction());
            }
        }
        else
        {
            Debug.Log("You'll interact w/ audio");
            if ((audioSource != null) && !playerMovement.isTalking)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogError("AudioSource is not set for this interactable");
            }

            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, this));
        }
    }

    private IEnumerator HandleSpecialInteraction()
    {
        // Mute all currently playing audio sources
        allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (var source in allAudioSources)
        {
            if (source.isPlaying && source != specificAudioSource)  // Exclude the specificAudioSource from being muted
            {
                source.Pause();
                // source.mute = true;
            }
        }

        // Play the specific sound
        specificAudioSource.Play();
        // Start the dialog while the sound is playing
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, this));
        // Wait for audio clip to finish playing before continuing coroutine
        yield return new WaitForSeconds(specificAudioSource.clip.length);

        // Unmute all previously playing audio sources
        foreach (var source in allAudioSources)
        {
            if (source != specificAudioSource)  // Ensure the specificAudioSource is not unmuted, as it should have finished playing
            {
                source.UnPause();
            }
        }
    }
}