using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour  // On Home scene
{
    public static AudioManager instance;  // Static
    public AudioSource audioSource;  // public or [SerializeField]- will show in inspector
    
    void Awake()  // Sets up a singleton/instance, void (default is private)- doesn't return anything
    {
        if (instance == null)  // Checks if an audio/instance is playing
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Makes the object persistent across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroys duplicates
        }
    }

    void Start()
    {
        if (audioSource != null && !audioSource.isPlaying)  // Play music if it exists and isn't already playing
        {
            audioSource.Play();
        }
    }

    public void ChangeMusic(AudioClip newClip)  // If new clip is diff than current one, then replace then play it, public- other scripts could call it directly
    {
        if (audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}