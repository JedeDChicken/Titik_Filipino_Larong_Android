using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicController : MonoBehaviour  // Change bg music of scenes
{
    public AudioClip sceneMusic;

    void Start()
    {
        if (AudioManager.instance != null && sceneMusic != null)  // If AudioManager exists and sceneMusic is set, then let AudioManager change to that clip
        {
            AudioManager.instance.ChangeMusic(sceneMusic);
        }
    }
}